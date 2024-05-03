namespace Pocus.Application.Dto;

public class GoalSettingsDto
{
    public int DayGoal { get; set; }
    public TimeSpan ResetTime { get; set; }
    public bool IgnoreWeekend { get; set; }
}
