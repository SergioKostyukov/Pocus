namespace Pocus.Application.Dto;

public class StatisticViewDto
{
    public DateTime Date { get; set; }
    public int TimeComplete { get; set; }
    public int PlansComplete { get; set; }
    public string TimeProductivityByHours { get; set; } = string.Empty;
    public string PlansProductivityByHours { get; set; } = string.Empty;
}
