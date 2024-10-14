using BookManager.Api.Domain;
using Microsoft.EntityFrameworkCore;

namespace BookManager.Api;

public class AppDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}