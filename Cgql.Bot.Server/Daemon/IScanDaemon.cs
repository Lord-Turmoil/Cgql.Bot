using Cgql.Bot.Model.Database;

namespace Cgql.Bot.Server.Daemon;

public interface IScanDaemon : IHostedService, IDisposable
{
    void AddTask(ScanTask task);
    void DropAllTasks();
}