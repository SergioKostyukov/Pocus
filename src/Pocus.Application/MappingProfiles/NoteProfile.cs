using AutoMapper;
using Pocus.Core.Entities;
using Pocus.Application.Dto;

namespace Pocus.Application.MappingProfiles;

internal class NoteProfile : Profile
{
    public NoteProfile()
    {
        CreateMap<Note, NoteDto>();
        CreateMap<NoteDto, Note>();
        CreateMap<NoteViewDto, Note>();
        CreateMap<ObjectTitleDto, Note>();
    }
}
