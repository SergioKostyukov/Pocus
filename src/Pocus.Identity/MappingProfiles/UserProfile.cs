using AutoMapper;
using Pocus.Core.Entities;
using Pocus.Identity.Dto;

namespace Pocus.Identity.MappingProfiles;

internal class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<RegisterDto, User>();
        CreateMap<User, UserDto>();
    }
}
