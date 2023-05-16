using Domain.Models;

namespace Application.Interfaces;

public interface IFilmListService
{
    Task AddFilmListAsync(FilmList filmList, CancellationToken cancellationToken = default);

    Task<int> CreateEmptyListAsync(CancellationToken cancellationToken = default);

    Task<bool> AddItemToListAsync(int listId, int itemId, CancellationToken cancellationToken = default);

    Task<bool> RemoveItemFromListAsync(int listId, int itemId, CancellationToken cancellationToken = default);

    Task<FilmList?> GetFilmListAsync(int id, CancellationToken cancellationToken = default);

    Task<IEnumerable<FilmList>> GetAllFilmListsAsync(CancellationToken cancellationToken = default);

    Task UpdateFilmListAsync(FilmList film, CancellationToken cancellationToken = default);

    Task DeleteFilmListAsync(FilmList film, CancellationToken cancellationToken = default);

    Task DeleteFilmListByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<bool> CheckIfFilmListExists(int id, CancellationToken cancellationToken = default);
}
