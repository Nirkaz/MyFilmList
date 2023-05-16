using Domain.Enums;

namespace Domain.Models;

public sealed class ListItem
{
    public int Id { get; set; }

    public Film Film { get; set; }

    public int Progress { get; set; }

    public ListItemStatus ListItemStatus { get; set; }

    public ReviewScore Score { get; set; }

    public DateTime ModifiedDate { get; set; } = DateTime.Now;

    public DateTime StartDate { get; set; } = DateTime.Now;

    public DateTime FinishDate { get; set; }
}
