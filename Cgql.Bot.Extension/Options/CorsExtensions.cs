using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Tonisoft.AspExtensions.Cors;

public static class CorsExtensions
{
    public static void AddCors(this WebApplicationBuilder builder, string section)
    {
        var corsOptions = new CorsOptions();
        builder.Configuration.GetRequiredSection(CorsOptions.CorsSection).Bind(corsOptions);
        if (corsOptions.Enable)
        {
            builder.Services.AddCors(options => {
                options.AddPolicy(
                    CorsOptions.CorsPolicyName,
                    policy => {
                        if (corsOptions.AllowAny)
                        {
                            policy.AllowAnyOrigin();
                        }
                        else
                        {
                            foreach (string origin in corsOptions.Origins)
                            {
                                policy.WithOrigins(origin);
                            }

                            policy.AllowCredentials();
                        }

                        policy.AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });
        }
    }
}