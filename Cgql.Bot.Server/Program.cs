using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using Cgql.Bot.Model.Database;
using Microsoft.EntityFrameworkCore;
using Tonisoft.AspExtensions.Cors;
using Tonisoft.AspExtensions.Module;

namespace Cgql.Bot.Server;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        // Add global configurations.
        AddGlobalConfigurations(builder);

        // Initialize database.
        builder.Services.AddUnitOfWork<PrimaryDbContext>();
        ConfigureDatabase<PrimaryDbContext>(builder.Services, builder.Configuration);

        // Add modules to the container.
        builder.Services.RegisterModules(typeof(Program));

        // Add Automapper.
        var autoMapperConfig = new MapperConfiguration(config => { config.AddProfile(new AutoMapperProfile()); });
        builder.Services.AddSingleton(autoMapperConfig.CreateMapper());

        // Add controllers.
        builder.Services.AddControllers().AddNewtonsoftJson();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Add Cors.
        builder.AddCors(CorsOptions.CorsSection);

        // Add Background Task.
        // TODO:

        WebApplication app = builder.Build();

        app.UseDefaultFiles();
        app.UseStaticFiles();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseRouting();
        app.UseCors(CorsOptions.CorsPolicyName);
        app.MapControllers();
        app.MapFallbackToFile("/index.html");

        app.Run();
    }

    private static void AddGlobalConfigurations(WebApplicationBuilder builder)
    {
        Configuration.BotId = builder.Configuration["BotId"] ?? throw new ArgumentNullException("BotId");
        Configuration.Profile = builder.Configuration["Profile"] ?? throw new ArgumentNullException("Profile");
        Configuration.Version = builder.Configuration["Version"] ?? throw new ArgumentNullException("Version");
        Configuration.ContentPath = builder.Configuration["ContentPath"] ?? throw new ArgumentNullException("ContentPath");
    }

    private static void ConfigureDatabase<TContext>(IServiceCollection services, IConfiguration configuration)
        where TContext : DbContext
    {
        string database = configuration.GetConnectionString("Database") ?? throw new Exception("Missing database");
        string connection = configuration.GetConnectionString("DefaultConnection") ??
                            throw new Exception("Missing database connection");

        switch (database)
        {
            case "MySQL":
                services.AddDbContext<TContext>(option => { option.UseMySQL(connection); });
                break;
            case "SQLite":
                services.AddDbContext<TContext>(option => { option.UseSqlite(connection); });
                break;
            default:
                throw new Exception($"Invalid database: {database}");
        }
    }
}