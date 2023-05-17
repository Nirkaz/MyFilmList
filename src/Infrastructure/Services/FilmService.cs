using Application.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services;

public class FilmService : IFilmService
{
    private readonly ILogger<FilmService> _logger;
    private readonly IRepository<Film> _filmRepository;

    public FilmService(ILogger<FilmService> logger,IRepository<Film> filmRepository) {
        _logger = logger;
        _filmRepository = filmRepository;
    }

    public async Task<bool> AddFilmAsync(Film film, CancellationToken cancellationToken = default) {
        if (film is null) {
            _logger.LogWarning("Film object is null");
            return false;
        }

        return await _filmRepository.AddAsync(film, cancellationToken);
    }

    public async Task<Film?> GetFilmAsync(int id, CancellationToken cancellationToken = default) {
        var film = await _filmRepository.GetAsync(id, cancellationToken);

        if (film is null) {
            _logger.LogWarning("Film with specified id: {FilmId} wasn't found in the database.", id);
        }

        return film;
    }

    public async Task<IEnumerable<Film>> GetAllFilmsAsync(CancellationToken cancellationToken = default) {
        var films = await _filmRepository.GetAllAsync(cancellationToken);

        if (films?.Any() != true) {
            _logger.LogWarning("There wasn't any films in the database.");
        }

        return films;
    }

    public async Task<bool> UpdateFilmAsync(Film film, CancellationToken cancellationToken = default) {
        if (film is null) {
            _logger.LogWarning("Film object is null");
            return false;
        }

        return await CheckIfFilmExists(film.Id, cancellationToken)
            && await _filmRepository.UpdateAsync(film, cancellationToken);
    }

    public async Task<bool> DeleteFilmAsync(Film film, CancellationToken cancellationToken = default) {
        if (film is null) {
            _logger.LogWarning("Film object is null");
            return false;
        }

        return await _filmRepository.DeleteAsync(film, cancellationToken);
    }

    public async Task<bool> DeleteFilmByIdAsync(int id, CancellationToken cancellationToken = default) {
        return await CheckIfFilmExists(id, cancellationToken)
            && await _filmRepository.DeleteByIdAsync(id, cancellationToken);
    }

    public async Task<bool> CheckIfFilmExists(int id, CancellationToken cancellationToken = default) {
        _logger.LogWarning("Film with specified id: {FilmId} wasn't found in the database.", id);
        return await _filmRepository.CheckIfExistsAsync(id, cancellationToken);
    }
}
