using Pocus.Application.Dto;

namespace Pocus.WebUI.Models;

public class PlanModel
{
    public required string UserId { get; set; }
    public List<PlanDto> Plans { get; set; }
}
