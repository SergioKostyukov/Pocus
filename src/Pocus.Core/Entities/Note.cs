namespace Pocus.Core.Entities;

public class Note
{
	public int Id { get; set; }
	public string UserId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
	public string Text { get; set; } = string.Empty;
	public bool IsArchived { get; set; } = true;
	public bool IsPinned { get; set; } = false;
}
