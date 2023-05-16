using System.ComponentModel.DataAnnotations;

namespace Domain.Enums;

public enum ListItemStatus
{
    [Display(Name = "Plan to Watch")]
    PlanToWatch,
    Watching,
    Completed,
    [Display(Name = "On Hold")]
    OnHold,
    Dropped
}
