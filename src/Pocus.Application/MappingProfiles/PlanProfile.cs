using AutoMapper;
using Pocus.Core.Entities;
using Pocus.Application.Dto;

namespace Pocus.Application.MappingProfiles;

internal class PlanProfile : Profile
{
    public PlanProfile()
    {
        CreateMap<Plan, PlanDto>();
        CreateMap<PlanDto, Plan>();
        CreateMap<PlanViewDto, Plan>();
        CreateMap<ObjectTitleDto, Plan>();
    }
}
