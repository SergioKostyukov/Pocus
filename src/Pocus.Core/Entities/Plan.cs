namespace Pocus.Core.Entities;

public class Plan
{
	public int Id { get; set; }
	public string UserId { get; set; } = string.Empty;
	public string Title { get; set; } = string.Empty;
	public string Text { get; set; } = string.Empty;
	public DateTime? NotificationTime { get; set; }
	public bool IsArchived { get; set; } = false;
	public bool IsPinned { get; set; } = false;
}
