using BookManager.Api.Domain;
using BookManager.Api.DTOs;

namespace BookManager.Api.Services;

public interface IBookService
{
    Task<List<Book>> GetBooksAsync();
    Task<Book?> GetBookAsync(Guid id);
    Task<Guid> CreateBookAsync(CreateBookRequest request);
}
