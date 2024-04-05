using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Tonisoft.AspExtensions.Module;
using Tonisoft.AspExtensions.Response;

namespace Cgql.Bot.Server.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class StatusController : BaseController<StatusController>
{
    public StatusController(IMapper mapper, ILogger<StatusController> logger) : base(mapper, logger)
    {
    }

    [HttpGet]
    public ApiResponse Ping()
    {
        return new OkResponse(new OkDto("Pong"));
    }

    [HttpGet]
    public ApiResponse Profile()
    {
        return new OkResponse(new OkDto(data: new {
            Configuration.Profile,
            Configuration.Version,
            Configuration.BotId
        }));
    }
}