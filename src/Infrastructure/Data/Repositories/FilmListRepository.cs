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

    public async Task AddAsync(FilmList entity, CancellationToken cancellationToken = default) {
        await _context.FilmLists.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<FilmList?> GetAsync(int id, CancellationToken cancellationToken = default) {
        var list = await _context.FilmLists.FindAsync(new object?[] { id }, cancellationToken: cancellationToken);

        if (list is not null) {
            await _context.Entry(list)
                .Collection(c => c.Items)
                .LoadAsync(cancellationToken);

            foreach (var item in list.Items)
            {
                await _context.Entry(item)
                    .Reference(r => r.Film)
                    .LoadAsync(cancellationToken);
            }
            
        }

        return list;
    }

    public async Task<IEnumerable<FilmList>> GetAllAsync(CancellationToken cancellationToken = default) {
        return await _context.FilmLists
            .Include(i => i.Items)
            .ThenInclude(i => i.Film)
            .ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(FilmList entity, CancellationToken cancellationToken = default) {
        _context.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(FilmList entity, CancellationToken cancellationToken = default) {
        _context.FilmLists.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteByIdAsync(int id, CancellationToken cancellationToken = default) {
        var entity = await _context.FilmLists.FindAsync(new object?[] { id }, cancellationToken: cancellationToken);
        _context.FilmLists.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> CheckIfExistsAsync(int id, CancellationToken cancellationToken = default) {
        return await _context.FilmLists.AnyAsync(list => list.Id == id, cancellationToken);
    }
}
