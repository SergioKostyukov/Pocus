namespace Pocus.Application.Dto;

public class GoalSettingsDto
{
    public int UserId { get; set; }
    public int DayGoal { get; set; }
    public TimeSpan ResetTime { get; set; }
    public bool IgnoreWeekend { get; set; }
}
