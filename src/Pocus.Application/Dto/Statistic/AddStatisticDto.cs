namespace Pocus.Application.Dto;

public class AddStatisticDto
{
    public string UserId { get; set; }
    public DateTime Date { get; set; }
    public int TimeComplete { get; set; }
    public int PlansComplete { get; set; }
    public bool IsIgnore { get; set; } = true;
    public string TimeProductivityByHours { get; set; } = string.Empty;
    public string PlansProductivityByHours { get; set; } = string.Empty;
}
