using BookManager.Api;
using BookManager.Api.DTOs;
using BookManager.Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IBookService, BookService>();

var assemblyName = typeof(Program).Assembly.GetName().Name;
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(
    c => c.UseNpgsql(connectionString, m => m.MigrationsAssembly(assemblyName)));
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

using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
if (context.Database.GetPendingMigrations().Any())
{
    await context.Database.MigrateAsync();
}

app.Run();