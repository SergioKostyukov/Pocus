using Pocus.Application.Dto;

namespace Pocus.WebUI.Models;

public class SessionActiveModel
{
    public required PlanViewDto Plan { get; set; }
    public NoteViewDto? Note { get; set; }
    public PlanViewDto? Habits { get; set; }
    public required SettingsDto Settings { get; set; }
}
