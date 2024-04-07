using Cgql.Bot.Model.Dto;

namespace Cgql.Bot.Server.Services;

public interface IResultService
{
    Task<CompleteResultDto> GetResult(long id, string key);
}