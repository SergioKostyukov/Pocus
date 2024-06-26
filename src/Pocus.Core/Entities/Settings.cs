﻿namespace Pocus.Core.Entities;
public class Settings
{
	public string UserId { get; set; } = string.Empty;
    public int WorkTime { get; set; }
	public int BreakTime { get; set; }
	public bool IsNotificationSound { get; set; } = true;
	public int DayGoal { get; set; }
	public TimeSpan ResetTime { get; set; }
	public bool IgnoreWeekend { get; set; } = false;
	public bool ThemeColor { get; set; } = true;
	public bool IgnoreHabits { get; set; } = false;
	public bool BlockSites { get; set; } = false;
}
