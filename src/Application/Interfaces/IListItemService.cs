using Domain.Enums;
using Domain.Models;

namespace Application.Interfaces;

public interface IListItemService
{
    Task<ListItem?> CreateListItemFromFilmAsync(Film film, CancellationToken cancellationToken = default);

    Task<ListItem?> CreateListItemFromFilmAsync(int filmId, CancellationToken cancellationToken = default);

    Task<bool> AddListItemAsync(ListItem item, CancellationToken cancellationToken = default);

    Task<ListItem?> GetListItemAsync(int id, CancellationToken cancellationToken = default);

    Task<IEnumerable<ListItem>> GetAllListItemsAsync(CancellationToken cancellationToken = default);

    Task<IEnumerable<ListItem>> GetListItemsByStatus(ListItemStatus status, CancellationToken cancellationToken = default);

    Task<bool> UpdateListItemAsync(ListItem item, CancellationToken cancellationToken = default);

    Task<bool> DeleteListItemAsync(ListItem item, CancellationToken cancellationToken = default);

    Task<bool> DeleteListItemByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<bool> CheckIfListItemExists(int id, CancellationToken cancellationToken = default);
}
