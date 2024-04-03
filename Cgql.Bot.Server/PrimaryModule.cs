using Cgql.Bot.Server.Services;
using Cgql.Bot.Server.Services.Impl;
using Tonisoft.AspExtensions.Module;

namespace Cgql.Bot.Server;

public class PrimaryModule : BaseModule
{
    public override IServiceCollection RegisterModule(IServiceCollection services)
    {
        services.AddScoped<IWebhookService, WebhookService>();

        return services;
    }
}