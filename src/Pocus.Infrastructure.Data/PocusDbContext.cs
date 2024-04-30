using Microsoft.EntityFrameworkCore;
using Pocus.Core.Entities;

namespace Pocus.Infrastructure.Data;

public class PocusDbContext : DbContext
{
    public PocusDbContext(DbContextOptions<PocusDbContext> options) : base(options) { }
    public PocusDbContext() { }
    public DbSet<User> Users { get; set; }
    public DbSet<Note> Notes { get; set; }
    public DbSet<Plan> Plans { get; set; }
    public DbSet<Settings> Settings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var assembly = typeof(PocusDbContext).Assembly;

        modelBuilder.ApplyConfigurationsFromAssembly(assembly);
    }
}
