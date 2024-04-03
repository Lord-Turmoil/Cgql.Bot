using Cgql.Bot.Model.Database;

namespace Cgql.Bot.Server.Services;

public interface IScanService
{
    public void AddTask(ScanTask task);
}