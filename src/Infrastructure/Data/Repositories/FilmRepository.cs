using Application.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public sealed class FilmRepository : IRepository<Film>
{
    private readonly ApplicationDbContext _context;

    public FilmRepository(ApplicationDbContext context) {
        _context = context;
    }

    public async Task<bool> AddAsync(Film entity, CancellationToken cancellationToken = default) {
        await _context.Films.AddAsync(entity, cancellationToken);
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<Film?> GetAsync(int id, CancellationToken cancellationToken = default) {
        return await _context.Films.FindAsync(new object?[] { id }, cancellationToken: cancellationToken);
    }

    public async Task<IEnumerable<Film>> GetAllAsync(CancellationToken cancellationToken = default) {
        return await _context.Films.ToListAsync(cancellationToken);
    }

    public async Task<bool> UpdateAsync(Film entity, CancellationToken cancellationToken = default) {
        _context.Update(entity);
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> DeleteAsync(Film entity, CancellationToken cancellationToken = default) {
        _context.Films.Remove(entity);
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> DeleteByIdAsync(int id, CancellationToken cancellationToken = default) {
        var entity = await _context.Films.FindAsync(new object?[] { id }, cancellationToken: cancellationToken);
        return entity is not null && await DeleteAsync(entity, cancellationToken);
    }

    public async Task<bool> CheckIfExistsAsync(int id, CancellationToken cancellationToken = default) {
        return await _context.Films.AnyAsync(film => film.Id == id, cancellationToken);
    }
}
