using Pocus.Application.Dto;

namespace Pocus.WebUI.Models;

public class NoteModel
{
    public required List<NoteDto> Notes { get; set; }
}
