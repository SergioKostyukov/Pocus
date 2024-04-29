using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pocus.Core.Entities;

namespace Pocus.Infrastructure.Data.EntityTypeConfiguration;

internal class SettingsEntityConfiguration : IEntityTypeConfiguration<Settings>
{
	public void Configure(EntityTypeBuilder<Settings> builder)
	{
		builder.HasKey(x => x.Id);

		builder.Property(x => x.UserId)
			.IsRequired();

		builder.Property(x => x.WorkTime)
			.IsRequired();

		builder.Property(x => x.BreakTime)
			.IsRequired();

		builder.Property(x => x.IsNotificationSound)
			.IsRequired();

		builder.Property(x => x.DayGoal)
			.IsRequired();

		builder.Property(x => x.ResetTime)
			.IsRequired();

		builder.Property(x => x.IgnoreWeekend)
			.IsRequired();

		builder.Property(x => x.ThemeColor)
			.IsRequired();

		builder.Property(x => x.IgnoreHabits)
			.IsRequired();

		builder.Property(x => x.BlockSites)
			.IsRequired();
	}
}
