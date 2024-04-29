using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pocus.Core.Entities;

namespace Pocus.Infrastructure.Data.EntityTypeConfiguration;

internal class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.HasKey(x => x.Id);

		builder.Property(x => x.Tag)
			.IsRequired();

		builder.Property(x => x.Email)
			.IsRequired();

		builder.Property(x => x.Password)
			.IsRequired();

		builder.Property(x => x.Notifications)
			.IsRequired();
	}
}
