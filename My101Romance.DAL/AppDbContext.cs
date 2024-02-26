using Microsoft.EntityFrameworkCore;

using My101Romance.Domain;

namespace My101Romance.DAL;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Card>? Card { get; set; }
}