using Pocus.Application.Dto;

namespace Pocus.Application.Interfaces;

public interface IPlanService : IObjectService
{
    public Task CreateDefault(string userId);
    public Task Create(PlanAddDto request);
    public Task<PlanDto> GetHabits(string userId);
    public Task<PlanViewDto> GetById(int planId);
    public Task<List<PlanDto>> GetArchived(string userId);
    public Task<List<PlanDto>> GetNotArchived(string userId);
    public Task Update(PlanUpdateDto request);
}
