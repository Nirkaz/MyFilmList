using Domain.Enums;
using Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data;

internal static class DataSeeder
{
    internal static void SeedFilms(EntityTypeBuilder<Film> entity) {
        var films = new Film[] {
            new Film {
                Id = 1,
                Title = "The Shawshank Redemption",
                Description = "Over the course of several years, two convicts form a friendship, seeking consolation and, eventually, redemption through basic compassion.",
                FilmStatus = FilmStatus.FinishedAiring,
                FilmType = FilmType.Movie,
                ReleaseDate = new DateTime(1994, 9, 10),
                Genre = GenreType.Drama
            },
            new Film {
                Id = 2,
                Title = "The Godfather",
                Description = "Don Vito Corleone, head of a mafia family, decides to hand over his empire to his youngest son Michael. However, his decision unintentionally puts the lives of his loved ones in grave danger.",
                FilmStatus = FilmStatus.FinishedAiring,
                FilmType = FilmType.Movie,
                ReleaseDate = new DateTime(1972, 3, 14),
                Genre = GenreType.Crime | GenreType.Drama
            },
            new Film {
                Id = 3,
                Title = "The Dark Knight",
                Description = "When the menace known as the Joker wreaks havoc and chaos on the people of Gotham, Batman must accept one of the greatest psychological and physical tests of his ability to fight injustice.",
                FilmStatus = FilmStatus.FinishedAiring,
                FilmType = FilmType.Movie,
                ReleaseDate = new DateTime(2008, 7, 14),
                Genre = GenreType.Action | GenreType.Crime | GenreType.Drama
            },
            new Film {
                Id = 4,
                Title = "12 Angry Men",
                Description = "The jury in a New York City murder trial is frustrated by a single member whose skeptical caution forces them to more carefully consider the evidence before jumping to a hasty verdict.",
                FilmStatus = FilmStatus.FinishedAiring,
                FilmType = FilmType.Movie,
                ReleaseDate = new DateTime(1957, 4, 10),
                Genre = GenreType.Crime | GenreType.Drama
            },
            new Film {
                Id = 5,
                Title = "Schindler's List",
                Description = "In German-occupied Poland during World War II, industrialist Oskar Schindler gradually becomes concerned for his Jewish workforce after witnessing their persecution by the Nazis.",
                FilmStatus = FilmStatus.FinishedAiring,
                FilmType = FilmType.Movie,
                ReleaseDate = new DateTime(1993, 11, 30),
                Genre = GenreType.Biography | GenreType.Drama | GenreType.History
            }
        };

        entity.HasData(films);
    }


}