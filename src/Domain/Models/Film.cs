using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Models;

public sealed class Film
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string? Description { get; set; }

    public FilmType FilmType { get; set; }

    public FilmStatus FilmStatus { get; set; }

    [Display(Name = "Release Date"), DataType(DataType.Date)]
    public DateTime? ReleaseDate { get; set; }

    public GenreType Genre { get; set; }
}
