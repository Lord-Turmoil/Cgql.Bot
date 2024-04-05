using Cgql.Bot.Extension.Email;
using Cgql.Bot.Model.Database;

namespace Cgql.Bot.Server.Daemon.Impl;

public class ApiDaemon : IApiDaemon
{
    private readonly ILogger<ApiDaemon> _logger;
    private readonly IEmailDaemon _emailService;

    public ApiDaemon(ILogger<ApiDaemon> logger, IEmailDaemon emailService)
    {
        _logger = logger;
        _emailService = emailService;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("API daemon online.");
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("API daemon offline.");
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        // Do nothing...
    }

    public void SendResult(ScanTask task)
    {
        if (task.Commit == null)
        {
            _logger.LogError("Task {Id} has no commit", task.Id);
            return;
        }
        if (task.Repository == null)
        {
            _logger.LogError("Task {Id} has no repository", task.Id);
            return;
        }

        string username = task.Commit.AuthorName;
        string repoName = task.Repository.FullName;
        string url = Configuration.RootUrl + $"/result/{task.Id}?key={task.Key}";
        string body = string.Format(File.ReadAllText("Template/ResultNotificationEmail.html")
            , username, repoName, url);

        _emailService.SendAsync(new EmailData {
            ToEmail = task.Commit.AuthorEmail,
            ToName = task.Commit.AuthorName,
            Subject = "CodeGraphQL Scan Result",
            Body = body
        });
    }
}