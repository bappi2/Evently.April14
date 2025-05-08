using Evently.Modules.Events.Application.Events;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Events.Presentation.Events;

internal static class GetEvent
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("events/{id:guid}", async (Guid id, ISender sender) =>
            {
                EventResponse? eventResponse = await sender.Send(new GetEventQuery(id)).ConfigureAwait(false);
                return eventResponse is not null ? Results.Ok(eventResponse) : Results.NotFound();
            })
            .WithTags(Tags.Events)
            .WithName("GetEvent");
    }
}
