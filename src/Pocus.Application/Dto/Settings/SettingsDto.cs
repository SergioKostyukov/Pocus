namespace Pocus.Application.Dto;

public class SettingsDto
{
    public string UserId { get; set; } = string.Empty;
    public int WorkTime { get; set; }
    public int BreakTime { get; set; }
    public bool IsNotificationSound { get; set; }
    public int DayGoal { get; set; }
    public TimeSpan ResetTime { get; set; }
    public bool IgnoreWeekend { get; set; }
    public bool ThemeColor { get; set; }
    public bool IgnoreHabits { get; set; }
    public bool BlockSites { get; set; }
}
