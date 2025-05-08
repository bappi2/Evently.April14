using System.ComponentModel.DataAnnotations.Schema;

namespace Evently.Modules.Events.Domain.Events;
#pragma warning disable CA1716

[Table("Events", Schema = "events")]
public sealed class Event
{
    [Column("id")]
    public Guid Id { get; set; }

    [Column("Title")]
    public string Title { get; set; } = string.Empty;

    [Column("Description")]
    public string Description { get; set; } = string.Empty;

    [Column("Location")]
    public string Location { get; set; } = string.Empty;

    [Column("StartsAtUtc")]
    public DateTime StartsAtUtc { get; set; }

    [Column("EndsAtUtc")]
    public DateTime? EndsAtUtc { get; set; }

    [Column("Status")]
    public EventStatus Status { get; set; } = EventStatus.Draft;
}
#pragma warning restore CA1716
