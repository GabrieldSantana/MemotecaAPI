using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context;
public class AppDbContext : DbContext
{
    public DbSet<QuoteModel> Pensamentos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}