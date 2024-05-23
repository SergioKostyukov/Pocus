using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pocus.Core.Entities;

namespace Pocus.Infrastructure.Data;

public class PocusDbContext : IdentityDbContext<User>
{
    public PocusDbContext(DbContextOptions<PocusDbContext> options) : base(options) { }
    public PocusDbContext() { }
    public DbSet<User> AspNetUsers { get; set; }
    public DbSet<Note> Notes { get; set; }
    public DbSet<Plan> Plans { get; set; }
    public DbSet<Settings> Settings { get; set; }
    public DbSet<Statistic> Statistic { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var assembly = typeof(PocusDbContext).Assembly;

        modelBuilder.ApplyConfigurationsFromAssembly(assembly);
    }
}
