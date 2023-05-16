using System.ComponentModel.DataAnnotations;

namespace Domain.Enums;

[Flags]
public enum GenreType
{
    Unknown = 0,
    Action = 1 << 0,
    Adult = 1 << 1,
    Adventure = 1 << 2,
    Animation = 1 << 3,
    Biography = 1 << 4,
    Comedy = 1 << 5,
    Crime = 1 << 6,
    Documentary = 1 << 7,
    Drama = 1 << 8,
    Family = 1 << 9,
    Fantasy = 1 << 10,
    [Display(Name = "Film-Noir")]
    FilmNoir = 1 << 11,
    [Display(Name = "Game-Show")]
    GameShow = 1 << 12,
    History = 1 << 13,
    Horror = 1 << 14,
    Music = 1 << 15,
    Musical = 1 << 16,
    Mystery = 1 << 17,
    News = 1 << 18,
    [Display(Name = "Reality-TV")]
    RealityTV = 1 << 19,
    Romance = 1 << 20,
    [Display(Name = "Sci-Fi")]
    SciFi = 1 << 21,
    Short = 1 << 22,
    Sport = 1 << 23,
    [Display(Name = "Talk-Show")]
    TalkShow = 1 << 24,
    Thriller = 1 << 25,
    War = 1 << 26,
    Western = 1 << 27
}
