namespace Pocus.Application.Dto;

public class SessionSettingsDto
{
    public int UserId { get; set; }
    public int WorkTime { get; set; }
    public int BreakTime { get; set; }
    public bool IsNotificationSound { get; set; }
}
