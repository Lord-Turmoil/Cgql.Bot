using AutoMapper;
using Cgql.Bot.Model.Database;
using Cgql.Bot.Model.Dto;
using Cgql.Bot.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Tonisoft.AspExtensions.Module;
using Tonisoft.AspExtensions.Response;

namespace Cgql.Bot.Server.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class WebhookController : BaseController<WebhookController>
{
    private readonly IRepositoryService _repoService;
    private readonly IWebhookService _service;

    public WebhookController(IMapper mapper, ILogger<WebhookController> logger, IWebhookService service,
        IRepositoryService repoService)
        : base(mapper, logger)
    {
        _service = service;
        _repoService = repoService;
    }

    [HttpPost]
    public async Task<ApiResponse> Webhook(
        [FromQuery(Name = "installer_id")] int installerId,
        [FromBody] WebhookRequest request)
    {
        _logger.LogInformation("Webhook received from installer {installerId}", installerId);
        try
        {
            ScanTask task = await _repoService.RequestNewTaskAsync(request, installerId);
            _service.AddTask(task);
            return new OkResponse(new OkDto(data: _mapper.Map<ScanTask, WebhookResponse>(task)));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to process webhook from installer {installerId}", installerId);
            return new InternalServerErrorResponse(new InternalServerErrorDto(e.Message));
        }
    }
}