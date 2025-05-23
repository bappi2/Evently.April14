using Evently.Modules.Events.Application.Abstractions.Data;
using Evently.Modules.Events.Domain.Events;
using MediatR;

namespace Evently.Modules.Events.Application.Events;

public sealed record CreateEventCommand(string Title, 
    string Description, string Location, DateTime StartsAtUtc, DateTime? EndsAtUtc) : IRequest<Guid>;


internal sealed class CreateEventCommandHandler(IEventRepository eventRepository, IUnitofWork unitofWork) : IRequestHandler<CreateEventCommand, Guid>
{
    public async Task<Guid> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        var @event = new Event
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Description = request.Description,
            Location = request.Location,
            StartsAtUtc = request.StartsAtUtc,
            EndsAtUtc = request.EndsAtUtc,
            Status = EventStatus.Draft
        };

        eventRepository.Insert(@event);
        await unitofWork.SaveChangesAsync(cancellationToken);
        return @event.Id;
    }
}

