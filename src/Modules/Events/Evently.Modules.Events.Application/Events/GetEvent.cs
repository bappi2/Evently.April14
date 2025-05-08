using System.Data.Common;
using Dapper;
using Evently.Modules.Events.Application.Abstractions.Data;
using Evently.Modules.Events.Domain.Events;
using MediatR;

namespace Evently.Modules.Events.Application.Events;
public sealed record GetEventQuery(Guid Id) : IRequest<EventResponse?>;
internal sealed class GetEventQueryHandler(IDbConnectionFactory  dbConnectionFactory) : IRequestHandler<GetEventQuery, EventResponse?>
{
    public async Task<EventResponse?> Handle(GetEventQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();
        const string sql =
            $"""
             SELECT
                 e.id AS {nameof(EventResponse.Id)},
                 e.title AS {nameof(EventResponse.Title)},
                 e.description AS {nameof(EventResponse.Description)},
                 e.location AS {nameof(EventResponse.Location)},
                 e.starts_at_utc AS {nameof(EventResponse.StartsAtUtc)},
                 e.ends_at_utc AS {nameof(EventResponse.EndsAtUtc)},
             FROM events.events e
             WHERE e.id = @EventId
             """;
        EventResponse? response = await connection.QueryFirstOrDefaultAsync(sql, request);
        return response;
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
