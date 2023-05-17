using Domain.Models;

namespace Application.Interfaces;

public interface IFilmListService
{
    Task<bool> AddFilmListAsync(FilmList filmList, CancellationToken cancellationToken = default);

    Task<FilmList?> CreateEmptyListAsync(CancellationToken cancellationToken = default);

    Task<bool> AddItemToListAsync(int listId, int itemId, CancellationToken cancellationToken = default);

    Task<bool> RemoveItemFromListAsync(int listId, int itemId, CancellationToken cancellationToken = default);

    Task<FilmList?> GetFilmListAsync(int id, CancellationToken cancellationToken = default);

    Task<IEnumerable<FilmList>> GetAllFilmListsAsync(CancellationToken cancellationToken = default);

    Task<bool> UpdateFilmListAsync(FilmList film, CancellationToken cancellationToken = default);

    Task<bool> DeleteFilmListAsync(FilmList film, CancellationToken cancellationToken = default);

    Task<bool> DeleteFilmListByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<bool> CheckIfFilmListExists(int id, CancellationToken cancellationToken = default);
}
