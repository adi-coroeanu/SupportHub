namespace SupportHub.Core.Interfaces;

public interface IRepository<T>
{
    public Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default);
    public Task SaveAsync(List<T> items,  CancellationToken cancellationToken = default);
}