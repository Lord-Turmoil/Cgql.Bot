using System.Text.RegularExpressions;
using Arch.EntityFrameworkCore.UnitOfWork;
using Cgql.Bot.Helper;
using Cgql.Bot.Model.Database;
using Cgql.Bot.Model.Dto;
using Newtonsoft.Json;

namespace Cgql.Bot.Server.Daemon.Impl;

public class ScanAgent
{
    private readonly ILogger<ScanDaemon> _logger;
    private readonly IUnitOfWork _unitOfWork;

    private string _workingDirectory = null!;
    private string _projectPath = null!;
    private string _resultPath = null!;

    public ScanAgent(ILogger<ScanDaemon> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public ResultDto Scan(ScanTask task)
    {
        try
        {
            task.StartedAt = DateTime.Now;
            if (!Configuration.Profile.Equals("Production"))
            {
                _logger.LogWarning("Running in Development mode, skipping scan");
                throw new Exception("Scan skipped in Development mode");
            }

            ScanPreamble(task);

            _workingDirectory = GetTempPath(task);
            if (Directory.Exists(_workingDirectory))
            {
                Directory.Delete(_workingDirectory, true);
            }

            Directory.CreateDirectory(_workingDirectory);

            _projectPath = Path.Join(_workingDirectory, task.Repository!.Name);
            _resultPath = Path.Join(_workingDirectory, "result.txt");

            return ScanImpl(task);
        }
        catch (Exception e)
        {
            task.Message = e.Message;
            return new ResultDto {
                Value = task,
                Status = ResultDto.ScanStatus.ExceptionThrown,
                BugCount = 0,
                QueryCount = 0,
                Message = e.Message
            };
        }
        finally
        {
            if (Directory.Exists(_workingDirectory))
            {
                Directory.Delete(_workingDirectory, true);
            }

            task.FinishedAt = DateTime.Now;
            _unitOfWork.GetRepository<ScanTask>().Update(task);
            _unitOfWork.SaveChanges();
        }
    }

    private void ScanPreamble(ScanTask task)
    {
        if (task.Repository == null)
        {
            throw new Exception("No repository information found.");
        }

        if (task.Commit == null)
        {
            throw new Exception("No commit information found.");
        }
    }

    private ResultDto ScanImpl(ScanTask task)
    {
        ResultDto? result;
        task.StartedAt = DateTime.Now;

        // Clone project.
        if ((result = CloneProject(task)) != null)
        {
            return result;
        }

        // Load configuration.
        if ((result = LoadConfiguration(task)) != null)
        {
            return result;
        }

        // TODO: Check if is desired branch.

        // Checkout branch.
        if ((result = CheckoutBranch(task)) != null)
        {
            return result;
        }

        // Run scan.
        if ((result = RunScanCommand(task)) != null)
        {
            return result;
        }

        // Generate report.
        ScanResultDto dto = GenerateResult();
        var scanResult = new ScanResult {
            Id = task.Id,
            Status = true,
            BugCount = dto.BugCount,
            QueryCount = dto.QueryCount,
            Data = JsonConvert.SerializeObject(dto)
        };
        _unitOfWork.GetRepository<ScanResult>().Insert(scanResult);
        task.Status = true;

        return new ResultDto {
            Value = task,
            Result = scanResult,
            Status = ResultDto.ScanStatus.Success,
            BugCount = 0,
            QueryCount = 0,
            Message = "Scan completed."
        };
    }

    private ResultDto? CloneProject(ScanTask task)
    {
        int retVal = ProcessHelper.Exec("git", "clone", task.Repository!.CloneUrl, _projectPath);
        if (retVal != 0)
        {
            return new ResultDto {
                Value = task,
                Status = ResultDto.ScanStatus.GitError,
                BugCount = 0,
                QueryCount = 0,
                Message = "Failed to clone repository."
            };
        }

        return null;
    }

    private ResultDto? CheckoutBranch(ScanTask task)
    {
        string branch = Path.GetFileName(task.Ref);
        int retVal = ProcessHelper.ExecAt(_projectPath, "git", "checkout", branch);
        if (retVal != 0)
        {
            return new ResultDto {
                Value = task,
                Status = ResultDto.ScanStatus.GitError,
                BugCount = 0,
                QueryCount = 0,
                Message = "Failed to checkout branch."
            };
        }

        return null;
    }

    private ResultDto? LoadConfiguration(ScanTask task)
    {
        // TODO: For now, there's no configuration needed.
        return null;
    }

    private ResultDto? RunScanCommand(ScanTask task)
    {
        int retVal = ProcessHelper.Exec("bash", "Template/scan.sh", Configuration.CgqlHome, _projectPath, _resultPath);
        return retVal switch {
            1 => new ResultDto {
                Value = task,
                Status = ResultDto.ScanStatus.ExecutionError,
                BugCount = 0,
                QueryCount = 0,
                Message = "Failed to build graph."
            },
            2 => new ResultDto {
                Value = task,
                Status = ResultDto.ScanStatus.ExecutionError,
                BugCount = 0,
                QueryCount = 0,
                Message = "Failed to scan graph."
            },
            _ => null
        };
    }

    private ScanResultDto GenerateResult()
    {
        var result = new ScanResultDto {
            Results = new List<SingleResultDto>(),
            TotalTime = -1
        };

        int queryCount = 0;
        int bugCount = 0;
        foreach (string line in File.ReadAllLines(_resultPath))
        {
            if (line.StartsWith('{'))
            {
                var dto = JsonConvert.DeserializeObject<SingleResultDtoDecoy>(line);
                if (dto == null)
                {
                    _logger.LogError("Failed to deserialize result: {result}", line);
                }
                else
                {
                    SingleResultDto real = dto.Real();
                    result.Results.Add(real);
                    queryCount++;
                    bugCount += real.Result.BugCount;
                }
            }
            else
            {
                Match match = Regex.Match(line, @"\d+");
                if (match.Success)
                {
                    result.TotalTime = int.Parse(match.Value);
                }
                else
                {
                    _logger.LogError("Failed to get total execution time in: {line}", line);
                }
            }
        }

        result.QueryCount = queryCount;
        result.BugCount = bugCount;

        return result;
    }

    private static string GetTempPath(ScanTask task)
    {
        return Path.Join(Configuration.TempPath, task.Id.ToString());
    }
}