using AutoMapper;
using Cgql.Bot.Model.Database;
using Cgql.Bot.Model.Dto;

namespace Cgql.Bot.Server;

public class AutoMapperProfile : MapperConfigurationExpression
{
    public AutoMapperProfile()
    {
        CreateMap<ScanTask, WebhookResponse>();
    }
}