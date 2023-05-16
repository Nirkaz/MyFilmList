using Domain.Models;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    internal DbSet<Film> Films { get; set; }
    internal DbSet<ListItem> ListItems { get; set; }
    internal DbSet<FilmList> FilmLists { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {
        Database.Migrate();
    }

    protected override void OnModelCreating(ModelBuilder builder) {
        base.OnModelCreating(builder);

        builder.Entity<ListItem>(entity => {
            entity.ToTable(nameof(ListItems));
        });

        builder.Entity<FilmList>(entity => {
            entity.ToTable(nameof(FilmLists));
        });

        builder.Entity<Film>(entity => {
            entity.ToTable(nameof(Films));
            DataSeeder.SeedFilms(entity);
        });
    }
}
