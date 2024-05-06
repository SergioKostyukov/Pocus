using Pocus.Application.Dto;

namespace Pocus.WebUI.Models;

public class ConcentrationModel
{
    public required List<ObjectTitleDto> Plans { get; set; }
    public required List<ObjectTitleDto> Notes { get; set; }
    public required PlanViewDto Habits { get; set; }
    public required SettingsDto Settings { get; set; }
}
