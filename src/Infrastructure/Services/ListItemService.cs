using Application.Interfaces;
using Domain.Models;

namespace Infrastructure.Services;

public class ListItemService : IListItemService
{
    private readonly IRepository<Film> _filmRepo;
    private readonly IRepository<ListItem> _listItemRepo;

    public ListItemService(IRepository<ListItem> listItemRepo, IRepository<Film> filmRepo) {
        _listItemRepo = listItemRepo;
        _filmRepo = filmRepo;
    }

    public async Task AddListItemAsync(ListItem item, CancellationToken cancellationToken = default)
        => await _listItemRepo.AddAsync(item, cancellationToken);

    public async Task<bool> CheckIfListItemExists(int id, CancellationToken cancellationToken = default)
        => await _listItemRepo.CheckIfExistsAsync(id, cancellationToken);
    
    public async Task<ListItem?> CreateListItemFromFilmAsync(Film film, CancellationToken cancellationToken = default) {
        if (!await _filmRepo.CheckIfExistsAsync(film.Id, cancellationToken))
            return null;

        var newItem = new ListItem() {
            Film = film
        };

        await _listItemRepo.AddAsync(newItem, cancellationToken);
        return newItem;
    }

    public async Task<ListItem?> CreateListItemFromFilmAsync(int filmId, CancellationToken cancellationToken = default) {
        if (!await _filmRepo.CheckIfExistsAsync(filmId, cancellationToken))
            return null;

        var newItem = new ListItem {
            Film = await _filmRepo.GetAsync(filmId, cancellationToken)
        };

        await _listItemRepo.AddAsync(newItem, cancellationToken);
        return newItem;
    }

    public async Task DeleteListItemAsync(ListItem item, CancellationToken cancellationToken = default)
        => await _listItemRepo.DeleteAsync(item, cancellationToken);

    public async Task DeleteListItemByIdAsync(int id, CancellationToken cancellationToken = default)
        => await _listItemRepo.DeleteByIdAsync(id, cancellationToken);

    public async Task<IEnumerable<ListItem>> GetAllListItemsAsync(CancellationToken cancellationToken = default)
        => await _listItemRepo.GetAllAsync(cancellationToken);

    public async Task<ListItem?> GetListItemAsync(int id, CancellationToken cancellationToken = default)
        => await _listItemRepo.GetAsync(id, cancellationToken);

    public async Task UpdateListItemAsync(ListItem item, CancellationToken cancellationToken = default)
        => await _listItemRepo.UpdateAsync(item, cancellationToken);
}
