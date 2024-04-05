using Cgql.Bot.Model.Database;
using Cgql.Bot.Model.Dto;

namespace Cgql.Bot.Server.Daemon;

public interface IApiDaemon : IHostedService, IDisposable
{
    void SendResult(ResultDto result);
}