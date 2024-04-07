using AutoMapper;
using Cgql.Bot.Model.Dto;
using Cgql.Bot.Server.Exceptions;
using Cgql.Bot.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Tonisoft.AspExtensions.Module;
using Tonisoft.AspExtensions.Response;

namespace Cgql.Bot.Server.Controllers;

[ApiController]
public class ResultController : BaseController<ResultController>
{
    private readonly IResultService _service;

    public ResultController(IMapper mapper, ILogger<ResultController> logger, IResultService service)
        : base(mapper, logger)
    {
        _service = service;
    }

    [HttpGet]
    [Route("api/[controller]/{id:long}")]
    public async Task<ApiResponse> GetResult([FromRoute] long id, [FromQuery] string key)
    {
        try
        {
            CompleteResultDto dto = await _service.GetResult(id, key);
            return new OkResponse(new OkDto(data: dto));
        }
        catch (ResultException e)
        {
            return e.Type switch {
                ResultException.Types.NotFound => new OkResponse(new NotFoundDto(e.Message)),
                ResultException.Types.Error => new OkResponse(new BadRequestDto(e.Message)),
                ResultException.Types.Other => new OkResponse(new InternalServerErrorDto(
                    $"It's not you, it's us: {e.Message}")),
                _ => new OkResponse(new InternalServerErrorResponse(e.Message))
            };
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Unhandled exception when getting result {id}:{key}", id, key);
            return new InternalServerErrorResponse(new InternalServerErrorDto("It's not you, it's us."));
        }
    }
}