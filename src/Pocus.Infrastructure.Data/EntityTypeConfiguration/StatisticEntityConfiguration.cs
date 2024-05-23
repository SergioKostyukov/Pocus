using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pocus.Core.Entities;

namespace Pocus.Infrastructure.Data.EntityTypeConfiguration;

internal class StatisticEntityConfiguration : IEntityTypeConfiguration<Statistic>
{
	public void Configure(EntityTypeBuilder<Statistic> builder)
	{
		builder.HasKey(x => x.Id);

		builder.Property(x => x.UserId)
			.IsRequired();

		builder.Property(x => x.Date)
			.IsRequired();

		builder.Property(x => x.TimeComplete)
			.IsRequired();

		builder.Property(x => x.PlansComplete)
			.IsRequired();

		builder.Property(x => x.IsIgnore)
			.IsRequired();

        builder.Property(x => x.TimeProductivityByHours)
            .IsRequired();

        builder.Property(x => x.PlansProductivityByHours)
            .IsRequired();
    }
}
