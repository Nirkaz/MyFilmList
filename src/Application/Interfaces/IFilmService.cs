using Domain.Models;

namespace Application.Interfaces;

public interface IFilmService
{
    Task<bool> AddFilmAsync(Film film, CancellationToken cancellationToken = default);

    Task<Film?> GetFilmAsync(int id, CancellationToken cancellationToken = default);

    Task<IEnumerable<Film>> GetAllFilmsAsync(CancellationToken cancellationToken = default);

    Task<bool> UpdateFilmAsync(Film film, CancellationToken cancellationToken = default);

    Task<bool> DeleteFilmAsync(Film film, CancellationToken cancellationToken = default);

    Task<bool> DeleteFilmByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<bool> CheckIfFilmExists(int id, CancellationToken cancellationToken = default);
}
