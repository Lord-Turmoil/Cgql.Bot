using Cgql.Bot.Model.Dto;
using Cgql.Bot.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Tonisoft.AspExtensions.Module;
using Tonisoft.AspExtensions.Response;

namespace Cgql.Bot.Server.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class WebhookController : BaseController<WebhookController>
{
    private IWebhookService _service;

    public WebhookController(ILogger<WebhookController> logger, IWebhookService service) : base(logger)
    {
        _service = service;
    }

    [HttpPost]
    public ApiResponse Webhook(
        [FromQuery(Name = "installer_id")] string installerId,
        [FromBody] WebhookRequest request)
    {
        _logger.LogInformation("Webhook received from installer {installerId}: {request}",
            installerId, JsonConvert.SerializeObject(request, Formatting.Indented));

        return new OkResponse(new OkDto());
    }
}