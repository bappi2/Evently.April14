namespace Evently.Modules.Events.Application.Abstractions.Data;

public interface IUnitofWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
