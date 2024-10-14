namespace BookManager.Api.Domain;

public class Book
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? Title { get; set; }
    public int PublishedYear { get; set; }
    public int Pages { get; set; }
    public string? Summary { get; set; }
}