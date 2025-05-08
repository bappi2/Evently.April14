using Microsoft.EntityFrameworkCore;

namespace Evently.Api.Extensions;

public static class MigrationExtentions
{
    internal static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();
        ApplyMigration<EventsDbContext>(scope);
    }

    private static void ApplyMigration<TDbContext>(IServiceScope scope)
        where TDbContext : DbContext
    {
        using TDbContext context = scope.ServiceProvider.GetRequiredService<TDbContext>();

        context.Database.Migrate();
    }
}
