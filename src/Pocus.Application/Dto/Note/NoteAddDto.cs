namespace Pocus.Application.Dto;

public class NoteAddDto
{
    public string UserId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public bool IsArchived { get; set; }
    public bool IsPinned { get; set; }
}
