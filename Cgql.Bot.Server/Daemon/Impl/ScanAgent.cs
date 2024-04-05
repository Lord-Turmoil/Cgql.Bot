using Arch.EntityFrameworkCore.UnitOfWork;
using Cgql.Bot.Helper;
using Cgql.Bot.Model.Database;
using Cgql.Bot.Model.Dto;

namespace Cgql.Bot.Server.Daemon.Impl;

public class ScanAgent
{
    private readonly ILogger<ScanDaemon> _logger;
    private readonly IUnitOfWork _unitOfWork;

    private string _workingDirectory;
    private string _projectPath;
    private string _resultPath;
    private string _scriptPath;

    public ScanAgent(ILogger<ScanDaemon> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public ScanResult Scan(ScanTask task)
    {
        try
        {
            task.StartedAt = DateTime.Now;

            ScanPreamble(task);

            _workingDirectory = GetTempPath(task);
            _projectPath = Path.Join(_workingDirectory, task.Repository!.Name);
            _resultPath = Path.Join(_workingDirectory, "result.txt");
            _scriptPath = Path.Join(_workingDirectory, "scan.sh");
            Directory.CreateDirectory(_workingDirectory);

            return ScanImpl(task);
        }
        catch (Exception e)
        {
            return new ScanResult {
                Value = task,
                Status = ScanResult.ScanStatus.ExceptionThrown,
                BugCount = 0,
                QueryCount = 0,
                Message = e.Message
            };
        }
        finally
        {
            if (Directory.Exists(_workingDirectory))
            {
                _logger.LogInformation("Cleaning up working directory {WorkingDirectory}", _workingDirectory);
                // Directory.Delete(_workingDirectory, true);
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

    private ScanResult ScanImpl(ScanTask task)
    {
        ScanResult? result;
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
        //if ((result = RunScanCommand(task)) != null)
        //{
        //    return result;
        //}

        // Generate report.
        //File.Copy(_resultPath, Path.Join("wwwroot", $"{task.Id}.txt"));

        task.Status = true;

        return new ScanResult {
            Value = task,
            Status = ScanResult.ScanStatus.Success,
            BugCount = 0,
            QueryCount = 0,
            Message = "Scan completed."
        };
    }

    private ScanResult? CloneProject(ScanTask task)
    {
        int retVal = ProcessHelper.Exec("git", "clone", task.Repository!.CloneUrl, _projectPath);
        if (retVal != 0)
        {
            return new ScanResult {
                Value = task,
                Status = ScanResult.ScanStatus.GitError,
                BugCount = 0,
                QueryCount = 0,
                Message = "Failed to clone repository."
            };
        }

        return null;
    }

    private ScanResult? CheckoutBranch(ScanTask task)
    {
        string branch = Path.GetFileName(task.Ref);
        int retVal = ProcessHelper.ExecAt(_projectPath, "git", "checkout", branch);
        if (retVal != 0)
        {
            return new ScanResult {
                Value = task,
                Status = ScanResult.ScanStatus.GitError,
                BugCount = 0,
                QueryCount = 0,
                Message = "Failed to checkout branch."
            };
        }

        return null;
    }

    private ScanResult? LoadConfiguration(ScanTask task)
    {
        // TODO: For now, there's no configuration needed.
        return null;
    }

    private ScanResult? RunScanCommand(ScanTask task)
    {
        File.WriteAllText(_resultPath, string.Format(File.ReadAllText("Template/Scan.sh"), _projectPath, _resultPath));
        int retVal = ProcessHelper.Exec("bash", _scriptPath);
        return retVal switch {
            1 => new ScanResult {
                Value = task,
                Status = ScanResult.ScanStatus.ExecutionError,
                BugCount = 0,
                QueryCount = 0,
                Message = "Failed to build graph."
            },
            2 => new ScanResult {
                Value = task,
                Status = ScanResult.ScanStatus.ExecutionError,
                BugCount = 0,
                QueryCount = 0,
                Message = "Failed to scan graph."
            },
            _ => null
        };
    }

    private string GetTempPath(ScanTask task)
    {
        return Path.Join(Path.GetTempPath(), task.Id.ToString());
    }
}