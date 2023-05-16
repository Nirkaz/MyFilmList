using Application.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public sealed class FilmListRepository : IRepository<FilmList>
{
    private readonly ApplicationDbContext _context;

    public FilmListRepository(ApplicationDbContext context) {
        _context = context;
    }

    public async Task<bool> AddAsync(FilmList entity, CancellationToken cancellationToken = default) {
        await _context.FilmLists.AddAsync(entity, cancellationToken);
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<FilmList?> GetAsync(int id, CancellationToken cancellationToken = default) {
        return await _context.FilmLists
            .Include(i => i.Items)
            .ThenInclude(i => i.Film)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<FilmList>> GetAllAsync(CancellationToken cancellationToken = default) {
        return await _context.FilmLists
            .Include(i => i.Items)
            .ThenInclude(i => i.Film)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> UpdateAsync(FilmList entity, CancellationToken cancellationToken = default) {
        _context.Update(entity);
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> DeleteAsync(FilmList entity, CancellationToken cancellationToken = default) {
        _context.FilmLists.Remove(entity);
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> DeleteByIdAsync(int id, CancellationToken cancellationToken = default) {
        var entity = await _context.FilmLists.FindAsync(new object?[] { id }, cancellationToken: cancellationToken);
        return entity is not null && await DeleteAsync(entity, cancellationToken);
    }

    public async Task<bool> CheckIfExistsAsync(int id, CancellationToken cancellationToken = default) {
        return await _context.FilmLists.AnyAsync(list => list.Id == id, cancellationToken);
    }
}
