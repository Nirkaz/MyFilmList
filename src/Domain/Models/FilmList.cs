namespace Domain.Models;

public sealed class FilmList
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public List<ListItem> Items { get; set; } = new();
}
