using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using BookManager.Api.DTOs;
using BookManager.Api.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register DynamoDB client and context
builder.Services.AddAWSService<IAmazonDynamoDB>();
builder.Services.AddScoped<IDynamoDBContext, DynamoDBContext>();

builder.Services.AddTransient<IBookService, BookService>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.MapGet("/", () => "Welcome to the Book API!");

// Minimal API to get all books
app.MapGet("/books", async (IBookService bookService) =>
{
    var books = await bookService.GetBooksAsync();
    return Results.Ok(books);
});

// Minimal API to get a book by ID
app.MapGet("/books/{id:guid}", async (IBookService bookService, Guid id) =>
{
    var book = await bookService.GetBookAsync(id);
    if (book == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(book);
});

// Minimal API to create a new book
app.MapPost("/books", async (IBookService bookService, CreateBookRequest request) =>
{
    var bookId = await bookService.CreateBookAsync(request);
    return Results.Created($"/books/{bookId}", request);
});

app.Run();