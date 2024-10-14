namespace BookManager.Api.DTOs;

public record CreateBookRequest(string Title, int PublishedYear, int Pages, string Summary);