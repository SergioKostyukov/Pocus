using Microsoft.EntityFrameworkCore;
using Pocus.Core.Entities;
using Pocus.Infrastructure.Data.EntityTypeConfiguration;

namespace Pocus.Infrastructure.Data;

public class PocusDbContext : DbContext
{
	public PocusDbContext(DbContextOptions<PocusDbContext> options) : base(options) { }
	public PocusDbContext() { }
	public DbSet<User> Users { get; set; }
	public DbSet<Note> Notes { get; set; }
	public DbSet<Core.Entities.Task> Tasks { get; set; }
	public DbSet<Settings> Settings { get; set; }

	public void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
		modelBuilder.ApplyConfiguration(new NoteEntityConfiguration());
		modelBuilder.ApplyConfiguration(new TaskEntityConfiguration());
		modelBuilder.ApplyConfiguration(new SettingsEntityConfiguration());
	}
}
