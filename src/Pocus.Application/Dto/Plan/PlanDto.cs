namespace Pocus.Application.Dto;

public class PlanDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public bool IsArchived { get; set; }
    public bool IsPinned { get; set; }
}
