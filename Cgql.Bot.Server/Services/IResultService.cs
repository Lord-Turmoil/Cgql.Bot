using Cgql.Bot.Model.Dto;

namespace Cgql.Bot.Server.Services;

public interface IResultService
{
    Task<ScanResultDto> GetResult(long id, string key);
}