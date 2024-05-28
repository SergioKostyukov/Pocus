namespace Pocus.WebUI.Models;

public class StatisticModel
{
    public DateValues Date { get; set; }
    public TableData TableData { get; set; }
    public List<GraphicData> StatisticForGraphic { get; set; }
}

public class DateValues
{
    int Day { get; set; }
    int Week { get; set; }
    int Month { get; set; }
    int Year { get; set; }
}

public class GraphicData
{
    string Date { get; set; } = string.Empty;
    int Hours { get; set; }
    int Plans { get; set; }
}

public class TableData
{
    int TotalTime { get; set; }
    int TotalPlans { get; set; }
    int AverageTime { get; set; }
    int AveragePlans { get; set; }
    string FavouriteTime { get; set; } = string.Empty;
    string MaxWorkTime { get; set; } = string.Empty;
}