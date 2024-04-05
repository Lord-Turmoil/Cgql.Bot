using Cgql.Bot.Model.Database;

namespace Cgql.Bot.Server.Daemon;

public interface IApiDaemon : IHostedService, IDisposable
{
    void SendResult(ScanTask task);
}