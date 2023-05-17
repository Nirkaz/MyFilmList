using Application.Interfaces;
using Domain.Enums;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services;

public class ListItemService : IListItemService
{
    private readonly ILogger<ListItemService> _logger;
    private readonly IRepository<Film> _filmRepo;
    private readonly IRepository<ListItem> _listItemRepo;

    public ListItemService(ILogger<ListItemService> logger, IRepository<ListItem> listItemRepo, IRepository<Film> filmRepo) {
        _logger = logger;
        _listItemRepo = listItemRepo;
        _filmRepo = filmRepo;
    }

    public async Task<bool> AddListItemAsync(ListItem item, CancellationToken cancellationToken = default) {
        if (item is null) {
            _logger.LogWarning("ListItem object is null");
            return false;
        }

        return await _listItemRepo.AddAsync(item, cancellationToken);
    }

    public async Task<ListItem?> CreateListItemFromFilmAsync(Film film, CancellationToken cancellationToken = default) {
        if (film is null) {
            _logger.LogWarning("Film object is null");
            return null;
        }

        var existsCheck = await _filmRepo.CheckIfExistsAsync(film.Id, cancellationToken);

        if (!existsCheck) {
            _logger.LogWarning("Film with id: {FilmId} wasn't found in the database.", film.Id);
            return null;
        }

        var newItem = new ListItem() {
            Film = film
        };

        if (!await _listItemRepo.AddAsync(newItem, cancellationToken)) {
            _logger.LogWarning("Film wasn't added into the database.");
            return null;
        }

        return newItem;
    }

    public async Task<ListItem?> CreateListItemFromFilmAsync(int filmId, CancellationToken cancellationToken = default) {
        var existsCheck = await _filmRepo.CheckIfExistsAsync(filmId, cancellationToken);

        if (!existsCheck) {
            _logger.LogWarning("Film with id: {FilmId} wasn't found in the database.", filmId);
            return null;
        }

        var film = await _filmRepo.GetAsync(filmId, cancellationToken);

        if (film is null) {
            _logger.LogWarning("Failed atempt to retrieve film with id {FilmId} from database", filmId);
            return null;
        }

        var newItem = await CreateListItemFromFilmAsync(film, cancellationToken);

        return newItem;
    }

    public async Task<ListItem?> GetListItemAsync(int id, CancellationToken cancellationToken = default) {
        var listItem = await _listItemRepo.GetAsync(id, cancellationToken);

        if (listItem is null) {
            _logger.LogWarning("ListItem with specified id: {ListItemId} wasn't found in the database.", id);
        }

        return listItem;
    }

    public async Task<IEnumerable<ListItem>> GetAllListItemsAsync(CancellationToken cancellationToken = default) {
        var listItems = await _listItemRepo.GetAllAsync(cancellationToken);

        if (listItems?.Any() != true) {
            _logger.LogWarning("There wasn't any list items in the database.");
        }

        return listItems;
    }

    public async Task<IEnumerable<ListItem>> GetListItemsByStatus(ListItemStatus status, CancellationToken cancellationToken = default) {
        var items = await GetAllListItemsAsync(cancellationToken);
        return items?.Where(x => x.ListItemStatus == status);
    }

    public async Task<bool> UpdateListItemAsync(ListItem item, CancellationToken cancellationToken = default) {
        if (item is null) {
            _logger.LogWarning("ListItem object is null");
            return false;
        }

        if (!await CheckIfListItemExists(item.Id, cancellationToken)) {
            _logger.LogWarning("ListItem with id: {ListItemId} wasn't found in the database.", item.Id);
            return false;
        }

        return await _listItemRepo.UpdateAsync(item, cancellationToken);
    }

    public async Task<bool> DeleteListItemAsync(ListItem item, CancellationToken cancellationToken = default) {
        if (item is null) {
            _logger.LogWarning("ListItem object is null");
            return false;
        }

        return await _listItemRepo.DeleteAsync(item, cancellationToken);
    }

    public async Task<bool> DeleteListItemByIdAsync(int id, CancellationToken cancellationToken = default) {
        if (!await CheckIfListItemExists(id, cancellationToken)) {
            _logger.LogWarning("ListItem with id: {ListItemId} wasn't found in the database.", id);
            return false;
        }

        return await _listItemRepo.DeleteByIdAsync(id, cancellationToken);
    }

    public async Task<bool> CheckIfListItemExists(int id, CancellationToken cancellationToken = default)
        => await _listItemRepo.CheckIfExistsAsync(id, cancellationToken);
}
