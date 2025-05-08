using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Evently.Modules.Events.Infrastructure.Database;

public class EventsDbContextFactory : IDesignTimeDbContextFactory<EventsDbContext>
{
    public EventsDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<EventsDbContext>();

        // Replace this connection string explicitly with your actual one.
        optionsBuilder.UseNpgsql("Host=evently.database;Port=5432;Database=evently;Username=postgres;Password=postgres;Include Error Detail=true")
            .ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));

        return new EventsDbContext(optionsBuilder.Options);
    }
}
