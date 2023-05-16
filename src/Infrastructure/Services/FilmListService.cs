using Application.Interfaces;
using Domain.Models;

namespace Infrastructure.Services;

public class FilmListService : IFilmListService
{
    //private readonly IRepository<Film> _filmRepo;
    private readonly IRepository<ListItem> _listItemRepo;
    private readonly IRepository<FilmList> _filmListRepo;

    public FilmListService(/*IRepository<Film> filmRepo, */IRepository<ListItem> listItemRepo, IRepository<FilmList> filmListRepo) {
        //_filmRepo = filmRepo;
        _listItemRepo = listItemRepo;
        _filmListRepo = filmListRepo;
    }

    public async Task AddFilmListAsync(FilmList filmList, CancellationToken cancellationToken = default)
        => await _filmListRepo.AddAsync(filmList, cancellationToken);

    public async Task<int> CreateEmptyListAsync(CancellationToken cancellationToken = default) {
        var filmList = new FilmList();
        await _filmListRepo.AddAsync(filmList, cancellationToken);
        return filmList.Id;
    }

    public async Task<bool> AddItemToListAsync(int listId, int itemId, CancellationToken cancellationToken = default) {
        if (!await CheckIfFilmListExists(listId, cancellationToken)) return false;
        if (!await _listItemRepo.CheckIfExistsAsync(itemId, cancellationToken)) return false;

        var list = await GetFilmListAsync(listId, cancellationToken);
        if (list is null) return false;

        var item = await _listItemRepo.GetAsync(itemId, cancellationToken);
        if (item is null) return false;

        list.Items.Add(item);
        await _filmListRepo.UpdateAsync(list, cancellationToken);

        return true;
    }

    public async Task<bool> RemoveItemFromListAsync(int listId, int itemId, CancellationToken cancellationToken = default) {
        if (!await CheckIfFilmListExists(listId, cancellationToken)) return false;
        if (!await _listItemRepo.CheckIfExistsAsync(itemId, cancellationToken)) return false;

        var list = await GetFilmListAsync(listId, cancellationToken);
        if (list is null) return false;

        var item = await _listItemRepo.GetAsync(itemId, cancellationToken);
        if (item is null) return false;

        list.Items.Remove(item);
        await _filmListRepo.UpdateAsync(list, cancellationToken);

        return true;
    }

    public async Task<bool> CheckIfFilmListExists(int id, CancellationToken cancellationToken = default)
        => await _filmListRepo.CheckIfExistsAsync(id, cancellationToken);

    public async Task DeleteFilmListAsync(FilmList filmList, CancellationToken cancellationToken = default)
        => await _filmListRepo.DeleteAsync(filmList, cancellationToken);

    public async Task DeleteFilmListByIdAsync(int id, CancellationToken cancellationToken = default)
        => await _filmListRepo.DeleteByIdAsync(id, cancellationToken);

    public async Task<IEnumerable<FilmList>> GetAllFilmListsAsync(CancellationToken cancellationToken = default)
        => await _filmListRepo.GetAllAsync(cancellationToken);

    public async Task<FilmList?> GetFilmListAsync(int id, CancellationToken cancellationToken = default)
        => await _filmListRepo.GetAsync(id, cancellationToken);

    public async Task UpdateFilmListAsync(FilmList film, CancellationToken cancellationToken = default)
        => await _filmListRepo.UpdateAsync(film, cancellationToken);
}
