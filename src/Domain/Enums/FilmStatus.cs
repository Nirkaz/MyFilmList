using System.ComponentModel.DataAnnotations;

namespace Domain.Enums;

public enum FilmStatus
{
    [Display(Name = "Not Yet Aired")]
    NotYetAired,
    Airing,
    [Display(Name = "Finished Airing")]
    FinishedAiring
}
