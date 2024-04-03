using Cgql.Bot.Model.Database;
using Cgql.Bot.Model.Dto;

namespace Cgql.Bot.Server.Services;

public interface IRepositoryService
{
    public Task<ScanTask> RequestNewTaskAsync(WebhookRequest request);
}