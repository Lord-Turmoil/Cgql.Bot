using Arch.EntityFrameworkCore.UnitOfWork;
using Cgql.Bot.Model.Database;
using Cgql.Bot.Server.Services;
using Cgql.Bot.Server.Services.Impl;
using Tonisoft.AspExtensions.Module;

namespace Cgql.Bot.Server;

public class PrimaryModule : BaseModule
{
    public override IServiceCollection RegisterModule(IServiceCollection services)
    {
        services.AddCustomRepository<Author, AuthorRepository>()
            .AddCustomRepository<Commit, CommitRepository>()
            .AddCustomRepository<Repo, RepoRepository>()
            .AddCustomRepository<ScanTask, ScanTaskRepository>()
            .AddCustomRepository<ScanResult, ScanResultRepository>();

        services.AddScoped<IWebhookService, WebhookService>()
            .AddScoped<IRepositoryService, RepositoryService>()
            .AddScoped<IResultService, ResultService>();

        return services;
    }
}