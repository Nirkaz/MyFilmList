using Domain.Models;

namespace Application.Interfaces;

public interface IFilmService
{
    Task AddFilmAsync(Film film, CancellationToken cancellationToken = default);

    Task<Film?> GetFilmAsync(int id, CancellationToken cancellationToken = default);

    Task<IEnumerable<Film>> GetAllFilmsAsync(CancellationToken cancellationToken = default);

    Task UpdateFilmAsync(Film film, CancellationToken cancellationToken = default);

    Task DeleteFilmAsync(Film film, CancellationToken cancellationToken = default);

    Task DeleteFilmByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<bool> CheckIfFilmExists(int id, CancellationToken cancellationToken = default);
}
