namespace Pocus.Core.Entities;

public class Note
{
	public int Id { get; set; }
	public int UserId { get; set; }
	public string Title { get; set; } = string.Empty;
	public string Text { get; set; } = string.Empty;
	public bool IsArchive { get; set; } = true;
	public bool IsPinned { get; set; } = false;
}
