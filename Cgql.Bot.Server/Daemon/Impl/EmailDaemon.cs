using Cgql.Bot.Extension.Email;
using Microsoft.Extensions.Options;

namespace Cgql.Bot.Server.Daemon.Impl;

public class EmailDaemon : IEmailDaemon
{
    private readonly ILogger<ScanDaemon> _logger;
    private readonly EmailOptions _options;

    public EmailDaemon(ILogger<ScanDaemon> logger, IOptions<EmailOptions> options)
    {
        _logger = logger;
        _options = options.Value;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Email daemon online.");
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Email daemon offline.");
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        // Do nothing...
    }

    public void Send(EmailData email)
    {
        try
        {
            EmailAgent.Draft(_options)
                .To(email.ToEmail)
                .Of(email.ToName)
                .WithSubject(email.Subject)
                .WithBody(email.Body)
                .Send();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to send email.");
        }
    }

    public Task SendAsync(EmailData email)
    {
        return Task.Run(() => Send(email));
    }
}