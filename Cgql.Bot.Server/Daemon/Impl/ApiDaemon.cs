using Cgql.Bot.Extension.Email;
using Cgql.Bot.Model.Database;
using Cgql.Bot.Model.Dto;

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

    public void SendResult(ResultDto result)
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
            case ResultDto.ScanStatus.Success:
                SendSuccessResult(result);
                break;
            default:
                SendFailedResult(result);
                break;
        }
    }

    private void SendSuccessResult(ResultDto result)
    {
        ScanTask task = result.Value;

        string url = Configuration.RootUrl + $"/result/{task.Id}?key={task.Key}";
        string body = string.Format(File.ReadAllText("Template/SuccessEmail.html"),
            task.Commit!.AuthorName,
            task.Repository!.HtmlUrl,
            task.Repository!.FullName,
            url);

        _emailService.SendAsync(new EmailData {
            ToEmail = task.Commit.AuthorEmail,
            ToName = task.Commit.AuthorName,
            Subject = "CodeGraphQL Scan Completed",
            Body = body
        });
    }

    private void SendFailedResult(ResultDto result)
    {
        ScanTask task = result.Value;

        string body = string.Format(File.ReadAllText("Template/FailedEmail.html"),
            task.Commit!.AuthorName,
            task.Repository!.HtmlUrl,
            task.Repository!.FullName,
            result.Status,
            result.Message);

        _emailService.SendAsync(new EmailData {
            ToEmail = task.Commit.AuthorEmail,
            ToName = task.Commit.AuthorName,
            Subject = "CodeGraphQL Scan Failed",
            Body = body
        });
    }
}