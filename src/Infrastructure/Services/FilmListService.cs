using Application.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services;

public class FilmListService : IFilmListService
{
    private readonly ILogger<FilmListService> _logger;
    private readonly IRepository<ListItem> _listItemRepo;
    private readonly IRepository<FilmList> _filmListRepo;

    public FilmListService(ILogger<FilmListService> logger, IRepository<ListItem> listItemRepo, IRepository<FilmList> filmListRepo) {
        _logger = logger;
        _listItemRepo = listItemRepo;
        _filmListRepo = filmListRepo;
    }

    public async Task<bool> AddFilmListAsync(FilmList filmList, CancellationToken cancellationToken = default) {
        if (filmList is null) {
            _logger.LogWarning("FilmList object is null");
            return false;
        }

        return await _filmListRepo.AddAsync(filmList, cancellationToken);
    }

    public async Task<FilmList?> CreateEmptyListAsync(CancellationToken cancellationToken = default) {
        var filmList = new FilmList();

        if (!await _filmListRepo.AddAsync(filmList, cancellationToken)) {
            _logger.LogWarning("FilmList wasn't added into the database.");
            return null;
        }

        return filmList;
    }

    public async Task<FilmList?> GetFilmListAsync(int id, CancellationToken cancellationToken = default) {
        var filmList = await _filmListRepo.GetAsync(id, cancellationToken);

        if (filmList is null) {
            _logger.LogWarning("FilmList with specified id: {FilmListId} wasn't found in the database.", id);
        }

        return filmList;
    }

    public async Task<IEnumerable<FilmList>> GetAllFilmListsAsync(CancellationToken cancellationToken = default) {
        var filmLists = await _filmListRepo.GetAllAsync(cancellationToken);

        if (filmLists?.Any() != true) {
            _logger.LogWarning("There wasn't any film lists in the database.");
        }

        return filmLists;
    }

    public async Task<bool> AddItemToListAsync(int listId, int itemId, CancellationToken cancellationToken = default) {
        if (!await CheckIfFilmListExists(listId, cancellationToken)) return false;
        if (!await _listItemRepo.CheckIfExistsAsync(itemId, cancellationToken)) return false;

        var list = await GetFilmListAsync(listId, cancellationToken);
        if (list is null) return false;

        var item = await _listItemRepo.GetAsync(itemId, cancellationToken);
        if (item is null) return false;

        list.Items.Add(item);
        return await _filmListRepo.UpdateAsync(list, cancellationToken);
    }

    public async Task<bool> RemoveItemFromListAsync(int listId, int itemId, CancellationToken cancellationToken = default) {
        if (!await CheckIfFilmListExists(listId, cancellationToken)) return false;
        if (!await _listItemRepo.CheckIfExistsAsync(itemId, cancellationToken)) return false;

        var list = await GetFilmListAsync(listId, cancellationToken);
        if (list is null) return false;

        var item = await _listItemRepo.GetAsync(itemId, cancellationToken);
        if (item is null) return false;

        list.Items.Remove(item);
        return await _filmListRepo.UpdateAsync(list, cancellationToken);
    }

    public async Task<bool> UpdateFilmListAsync(FilmList filmList, CancellationToken cancellationToken = default) {
        if (filmList is null) {
            _logger.LogWarning("FilmList object is null");
            return false;
        }

        return await CheckIfFilmListExists(filmList.Id, cancellationToken)
            && await _filmListRepo.UpdateAsync(filmList, cancellationToken);
    }

    public async Task<bool> DeleteFilmListAsync(FilmList filmList, CancellationToken cancellationToken = default) {
        if (filmList is null) {
            _logger.LogWarning("FilmList object is null");
            return false;
        }

        return await _filmListRepo.DeleteAsync(filmList, cancellationToken);
    }

    public async Task<bool> DeleteFilmListByIdAsync(int id, CancellationToken cancellationToken = default) {
        return await CheckIfFilmListExists(id, cancellationToken)
            && await _filmListRepo.DeleteByIdAsync(id, cancellationToken);
    }

    public async Task<bool> CheckIfFilmListExists(int id, CancellationToken cancellationToken = default) {
        _logger.LogWarning("FilmList with specified id: {FilmListId} wasn't found in the database.", id);
        return await _filmListRepo.CheckIfExistsAsync(id, cancellationToken);
    }
}
