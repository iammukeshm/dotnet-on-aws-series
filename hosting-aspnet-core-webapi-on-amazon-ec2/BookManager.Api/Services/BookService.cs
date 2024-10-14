using BookManager.Api.Domain;
using BookManager.Api.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BookManager.Api.Services;

public class BookService(AppDbContext dbContext) : IBookService
{
    public async Task<Guid> CreateBookAsync(CreateBookRequest request)
    {
        var book = new Book()
        {
            Pages = request.Pages,
            Title = request.Title,
            Summary = request.Summary,
            PublishedYear = request.PublishedYear
        };
        await dbContext.AddAsync(book);
        await dbContext.SaveChangesAsync();
        return book.Id;
    }

    public async Task<Book?> GetBookAsync(Guid id)
    {
        var book = await dbContext.Books.FindAsync(id);
        return book ?? null;
    }

    public async Task<List<Book>> GetBooksAsync()
    {
        var books = await dbContext.Books.ToListAsync();
        return books;
    }
}
