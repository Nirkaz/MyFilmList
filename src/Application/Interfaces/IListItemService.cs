using Domain.Models;

namespace Application.Interfaces;

public interface IListItemService
{
    Task<ListItem?> CreateListItemFromFilmAsync(Film film, CancellationToken cancellationToken = default);

    Task<ListItem?> CreateListItemFromFilmAsync(int filmId, CancellationToken cancellationToken = default);

    Task AddListItemAsync(ListItem item, CancellationToken cancellationToken = default);

    Task<ListItem?> GetListItemAsync(int id, CancellationToken cancellationToken = default);

    Task<IEnumerable<ListItem>> GetAllListItemsAsync(CancellationToken cancellationToken = default);

    Task UpdateListItemAsync(ListItem item, CancellationToken cancellationToken = default);

    Task DeleteListItemAsync(ListItem item, CancellationToken cancellationToken = default);

    Task DeleteListItemByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<bool> CheckIfListItemExists(int id, CancellationToken cancellationToken = default);
}
