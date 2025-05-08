using Evently.Modules.Events.Application.Events;
using Evently.Modules.Events.Domain.Events;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Events.Presentation.Events;

internal static class CreateEvent
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("events", async (Request request, ISender sender) => { 
            var command = new CreateEventCommand(
            request.Title,
            request.Description,
            request.Location,
            request.StartsAtUtc,
            request.EndsAtUtc);
                
            Guid eventId = await sender.Send(command);
            return Results.Ok(eventId);
        })
        .WithName("CreateEvent")
        .Produces<Event>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest)
        .WithTags(Tags.Events);
    }
}

internal sealed class Request
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public DateTime StartsAtUtc { get; set; }
    public DateTime? EndsAtUtc { get; set; }
}
