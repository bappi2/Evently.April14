using Evently.Modules.Events.Api.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace Evently.Modules.Events.Api.Events;

public static class GetEvent
{
    public static void MapEndpoint(IEndpointRouteBuilder routes)
    {
        routes.MapGet("events/{id:guid}", async (Guid id, EventsDbContext repository) =>
            {
                EventResponse? @event = await repository.Events
                    .Where(e => e.Id == id)
                    .Select(e => new EventResponse(
                        e.Id,
                        e.Title,
                        e.Description,
                        e.Location,
                        e.StartsAtUtc,
                        e.EndsAtUtc,
                        e.Status))
                    .SingleOrDefaultAsync()
                    .ConfigureAwait(false);

            return @event is not null ? Results.Ok(@event) : Results.NotFound();
        })
        .WithTags("Events")
        .WithName("GetEvent")
        .Produces<Event>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}

public sealed record EventResponse(
    Guid Id,
    string Title,
    string Description,
    string Location,
    DateTime StartsAtUtc,
    DateTime? EndsAtUtc,
    EventStatus Status);
