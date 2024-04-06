using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using Cgql.Bot.Model.Database;
using Cgql.Bot.Model.Dto;
using Cgql.Bot.Server.Exceptions;
using Newtonsoft.Json;
using Tonisoft.AspExtensions.Module;

namespace Cgql.Bot.Server.Services.Impl;

public class ResultService : BaseService<ResultService>, IResultService
{
    private readonly IRepository<ScanTask> _taskRepo;
    private readonly IRepository<ScanResult> _resultRepo;

    public ResultService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ILogger<ResultService> logger,
        IRepository<ScanTask> taskRepo,
        IRepository<ScanResult> resultRepo)
        : base(unitOfWork, mapper, logger)
    {
        _taskRepo = taskRepo;
        _resultRepo = resultRepo;
    }

    public async Task<ScanResultDto> GetResult(long id, string key)
    {
        ScanTask? task = await _taskRepo.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(id) && x.Key.Equals(key));
        if (task == null)
        {
            throw new ResultException(ResultException.Types.NotFound, "Requested scan result doesn't exist");
        }

        if (!task.Finished)
        {
            throw new ResultException(ResultException.Types.Error, "Requested scan not completed yet");
        }

        if (task.Status == false)
        {
            throw new ResultException(ResultException.Types.Error,
                $"Requested scan encountered one or more errors: {task.Message}");
        }

        ScanResult? result = await _resultRepo.FindAsync(task.Id);
        if (result?.Data == null)
        {
            throw new Exception("Request scan doesn't have a result");
        }

        ScanResultDto? dto = JsonConvert.DeserializeObject<ScanResultDto>(result.Data);
        if (dto == null)
        {
            throw new Exception("Failed to get scan result");
        }

        return dto;
    }
}