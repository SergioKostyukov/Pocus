using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Pocus.Infrastructure.Data.EntityTypeConfiguration;

internal class TaskEntityConfiguration : IEntityTypeConfiguration<Core.Entities.Task>
{
	public void Configure(EntityTypeBuilder<Core.Entities.Task> builder)
	{
		builder.HasKey(x => x.Id);

		builder.Property(x => x.UserId)
			.IsRequired();

		builder.Property(x => x.Title)
			.IsRequired();

		builder.Property(x => x.Text)
			.IsRequired();

		builder.Property(x => x.NotificationTime);

		builder.Property(x => x.IsArchive)
			.IsRequired();

		builder.Property(x => x.IsPinned)
			.IsRequired();
	}
}
