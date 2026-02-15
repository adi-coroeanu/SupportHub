namespace SupportHub.Core.Interfaces;

public interface IRepository<T>
{
    public Task<List<T>> GetAllAsync();
    public Task SaveAsync(List<T> items);
}