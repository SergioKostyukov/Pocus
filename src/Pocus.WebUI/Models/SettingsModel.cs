using Pocus.Application.Dto;

namespace Pocus.WebUI.Models;

public class SettingsModel
{
    public string UserId { get; set; } = string.Empty;
    public SessionSettingsDto SessionSettings { get; set; } = new SessionSettingsDto();
    public GoalSettingsDto GoalSettings { get; set; } = new GoalSettingsDto();
    public OtherSettingsDto OtherSettings { get; set; } = new OtherSettingsDto();
}
