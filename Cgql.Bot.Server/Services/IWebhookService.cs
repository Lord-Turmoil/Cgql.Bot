using Cgql.Bot.Model.Database;

namespace Cgql.Bot.Server.Services;

public interface IWebhookService
{
    public void AddTask(ScanTask task);
}