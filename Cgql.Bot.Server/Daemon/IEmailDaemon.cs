using Cgql.Bot.Extension.Email;

namespace Cgql.Bot.Server.Daemon;

public interface IEmailDaemon : IHostedService, IDisposable
{
    void Send(EmailData email);
    Task SendAsync(EmailData email);
}