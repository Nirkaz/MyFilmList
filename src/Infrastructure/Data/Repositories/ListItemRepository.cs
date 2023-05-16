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

    public async Task AddAsync(ListItem entity, CancellationToken cancellationToken = default) {
        await _context.ListItems.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<ListItem?> GetAsync(int id, CancellationToken cancellationToken = default) {
        var item = await _context.ListItems.FindAsync(new object?[] { id }, cancellationToken: cancellationToken);

        if (item is not null) {
            await _context.Entry(item)
                .Reference(r => r.Film)
                .LoadAsync(cancellationToken);
        }

        return item;
    }

    public async Task<IEnumerable<ListItem>> GetAllAsync(CancellationToken cancellationToken = default) {
        return await _context.ListItems
            .Include(i => i.Film)
            .ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(ListItem entity, CancellationToken cancellationToken = default) {
        _context.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(ListItem entity, CancellationToken cancellationToken = default) {
        _context.ListItems.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteByIdAsync(int id, CancellationToken cancellationToken = default) {
        var entity = await _context.ListItems.FindAsync(new object?[] { id }, cancellationToken: cancellationToken);
        _context.ListItems.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> CheckIfExistsAsync(int id, CancellationToken cancellationToken = default) {
        return await _context.ListItems.AnyAsync(item => item.Id == id, cancellationToken);
    }
}
