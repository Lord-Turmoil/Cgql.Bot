using Cgql.Bot.Extension.Email;
using Cgql.Bot.Model.Database;
using Cgql.Bot.Model.Dto;
using System.Threading.Tasks;

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

    public void SendResult(ScanResult result)
    {
        ScanTask task = result.Value;
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

        switch (result.Status)
        {
            case ScanResult.ScanStatus.Success:
                SendSuccessResult(result);
                break;
            default:
                SendFailedResult(result);
                break;
        }
    }

    private void SendSuccessResult(ScanResult result)
    {
        ScanTask task = result.Value;

        string username = task.Commit!.AuthorName;
        string repoName = task.Repository!.FullName;
        string url = Configuration.RootUrl + $"/result/{task.Id}?key={task.Key}";
        string body = string.Format(File.ReadAllText("Template/SuccessEmail.html")
            , username, repoName, url);

        _emailService.SendAsync(new EmailData {
            ToEmail = task.Commit.AuthorEmail,
            ToName = task.Commit.AuthorName,
            Subject = "CodeGraphQL Scan Completed",
            Body = body
        });
    }

    private void SendFailedResult(ScanResult result)
    {
        ScanTask task = result.Value;

        string username = task.Commit!.AuthorName;
        string repoName = task.Repository!.FullName;
        string body = string.Format(File.ReadAllText("Template/FailedEmail.html")
            , username, repoName, result.Status, result.Message);

        _emailService.SendAsync(new EmailData {
            ToEmail = task.Commit.AuthorEmail,
            ToName = task.Commit.AuthorName,
            Subject = "CodeGraphQL Scan Failed",
            Body = body
        });
    }
}