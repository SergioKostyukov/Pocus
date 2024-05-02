using AutoMapper;
using Pocus.Core.Entities;
using Pocus.Application.Dto;

namespace Pocus.Application.MappingProfiles;

internal class SettingsProfile : Profile
{
    public SettingsProfile()
    {
        CreateMap<Settings, SettingsDto>();
    }
}
