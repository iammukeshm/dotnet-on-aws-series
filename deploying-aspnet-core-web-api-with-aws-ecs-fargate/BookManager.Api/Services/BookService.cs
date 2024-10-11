using Amazon.DynamoDBv2.DataModel;
using BookManager.Api.Domain;
using BookManager.Api.DTOs;

namespace BookManager.Api.Services;

public class BookService : IBookService
{
    private readonly IDynamoDBContext dynamoDBContext;
    public BookService(IDynamoDBContext dynamoDBContext)
    {
        this.dynamoDBContext = dynamoDBContext;
    }

    public async Task<Guid> CreateBookAsync(CreateBookRequest request)
    {
        var book = new Book()
        {
            Pages = request.Pages,
            Title = request.Title,
            Summary = request.Summary,
            PublishedYear = request.PublishedYear
        };
        await dynamoDBContext.SaveAsync(book);
        return book.Id;
    }

    public async Task<Book> GetBookAsync(Guid id)
    {
        return await dynamoDBContext.LoadAsync<Book>(id);
    }

    public async Task<List<Book>> GetBooksAsync()
    {
        var books = await dynamoDBContext.ScanAsync<Book>(new List<ScanCondition>()).GetRemainingAsync();
        return books;
    }
}
