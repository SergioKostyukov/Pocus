using Pocus.Application.Dto;

namespace Pocus.WebUI.Models;

public class ArchiveModel
{
    public required List<PlanDto> PlansList { get; set; }
    public required List<NoteDto> NotesList { get; set; }

}
