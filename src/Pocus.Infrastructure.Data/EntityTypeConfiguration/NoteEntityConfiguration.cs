using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pocus.Core.Entities;

namespace Pocus.Infrastructure.Data.EntityTypeConfiguration;

internal class NoteEntityConfiguration : IEntityTypeConfiguration<Note>
{
	public void Configure(EntityTypeBuilder<Note> builder)
	{
		builder.HasKey(x => x.Id);

		builder.Property(x => x.UserId)
			.IsRequired();

		builder.Property(x => x.Title)
			.IsRequired();

		builder.Property(x => x.Text)
			.IsRequired();

		builder.Property(x => x.IsArchived)
			.IsRequired();

		builder.Property(x => x.IsPinned)
			.IsRequired();
	}
}
