using System.Reflection;
using Evently.Modules.Events.Application.Abstractions.Data;
using Evently.Modules.Events.Domain.Events;
using Evently.Modules.Events.Infrastructure.Data;
using Evently.Modules.Events.Infrastructure.Database;
using Evently.Modules.Events.Infrastructure.Events;
using Evently.Modules.Events.Presentation.Events;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace Evently.Modules.Events.Infrastructure;

public static class EventsModule
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        EventsEndpoints.MapEndpoints(app);
    }
    public static IServiceCollection AddEventsModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssembly(Application.AssemblyReference.Assembly));
        services.AddInfrastructure(configuration);
        return services;
    }
    private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string databaseConnectionString = configuration.GetConnectionString("Database");
        NpgsqlDataSource dataSource = new NpgsqlDataSourceBuilder(databaseConnectionString)
            .Build();
        services.AddSingleton(dataSource);
        services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();
        
        services.AddDbContext<EventsDbContext>(options =>
            options.UseNpgsql(databaseConnectionString, 
                    npgsqlOptions => npgsqlOptions.MigrationsHistoryTable(
                        HistoryRepository.DefaultTableName, SchemaNames.Events))
                .UseSnakeCaseNamingConvention());
        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<IUnitofWork>(provider => provider.GetRequiredService<EventsDbContext>());
    }
}
