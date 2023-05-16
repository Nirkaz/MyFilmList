namespace Application.Interfaces;

public interface IRepository<T> where T : class
{
    Task AddAsync(T entity, CancellationToken cancellationToken = default);

    Task<T?> GetAsync(int id, CancellationToken cancellationToken = default);

    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);

    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);

    Task DeleteAsync(T entity, CancellationToken cancellationToken = default);

    Task DeleteByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<bool> CheckIfExistsAsync(int id, CancellationToken cancellationToken = default);
}
