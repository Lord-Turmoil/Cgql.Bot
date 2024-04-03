using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using Cgql.Bot.Model.Database;
using Cgql.Bot.Server.Daemon;
using Tonisoft.AspExtensions.Module;

namespace Cgql.Bot.Server.Services.Impl;

public class WebhookService : BaseService<WebhookService>, IWebhookService
{
    private readonly IScanDaemon _daemon;

    public WebhookService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<WebhookService> logger, IScanDaemon daemon)
        : base(unitOfWork, mapper, logger)
    {
        _daemon = daemon;
    }

    public void AddTask(ScanTask task)
    {
        _daemon.AddTask(task);
    }
}