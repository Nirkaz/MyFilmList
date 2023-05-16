using Application.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public sealed class ListItemRepository : IRepository<ListItem>
{
    private readonly ApplicationDbContext _context;

    public ListItemRepository(ApplicationDbContext context) {
        _context = context;
    }

    public async Task<bool> AddAsync(ListItem entity, CancellationToken cancellationToken = default) {
        await _context.ListItems.AddAsync(entity, cancellationToken);
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<ListItem?> GetAsync(int id, CancellationToken cancellationToken = default) {
        return await _context.ListItems
            .Include(i => i.Film)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<ListItem>> GetAllAsync(CancellationToken cancellationToken = default) {
        return await _context.ListItems
            .Include(i => i.Film)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> UpdateAsync(ListItem entity, CancellationToken cancellationToken = default) {
        _context.Update(entity);
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> DeleteAsync(ListItem entity, CancellationToken cancellationToken = default) {
        _context.ListItems.Remove(entity);
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> DeleteByIdAsync(int id, CancellationToken cancellationToken = default) {
        var entity = await _context.ListItems.FindAsync(new object?[] { id }, cancellationToken: cancellationToken);
        return entity is not null && await DeleteAsync(entity, cancellationToken);
    }

    public async Task<bool> CheckIfExistsAsync(int id, CancellationToken cancellationToken = default) {
        return await _context.ListItems.AnyAsync(item => item.Id == id, cancellationToken);
    }
}
