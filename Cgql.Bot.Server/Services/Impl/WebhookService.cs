using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using Tonisoft.AspExtensions.Module;

namespace Cgql.Bot.Server.Services.Impl;

public class WebhookService : BaseService<WebhookService>, IWebhookService
{
    public WebhookService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<WebhookService> logger)
        : base(unitOfWork, mapper, logger)
    {
    }
}