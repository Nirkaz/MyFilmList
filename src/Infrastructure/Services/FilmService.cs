using Application.Interfaces;
using Domain.Models;

namespace Infrastructure.Services;

public class FilmService : IFilmService
{
    private readonly IRepository<Film> _filmRepository;

    public FilmService(IRepository<Film> filmRepository) {
        _filmRepository = filmRepository;
    }

    public async Task AddFilmAsync(Film film, CancellationToken cancellationToken = default) 
        => await _filmRepository.AddAsync(film, cancellationToken);

    public async Task<Film?> GetFilmAsync(int id, CancellationToken cancellationToken = default) 
        => await _filmRepository.GetAsync(id, cancellationToken);

    public async Task<IEnumerable<Film>> GetAllFilmsAsync(CancellationToken cancellationToken = default) 
        => await _filmRepository.GetAllAsync(cancellationToken);

    public async Task UpdateFilmAsync(Film film, CancellationToken cancellationToken = default) 
        => await _filmRepository.UpdateAsync(film, cancellationToken);

    public async Task DeleteFilmAsync(Film film, CancellationToken cancellationToken = default) 
        => await _filmRepository.DeleteAsync(film, cancellationToken);

    public async Task DeleteFilmByIdAsync(int id, CancellationToken cancellationToken = default) 
        => await _filmRepository.DeleteByIdAsync(id, cancellationToken);

    public async Task<bool> CheckIfFilmExists(int id, CancellationToken cancellationToken = default) 
        => await _filmRepository.CheckIfExistsAsync(id, cancellationToken);
}
