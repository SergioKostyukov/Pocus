using Pocus.Application.Dto;

namespace Pocus.Application.Interfaces;

public interface IPlanService
{
    public Task CreateDefault(int userId);
    public Task Create(PlanDto request);
    public Task<PlanDto?> GetHabits(int userId);
    public Task<PlanViewDto?> GetById(int planId);
    public Task<List<PlanDto>?> GetArchived(int userId);
    public Task<List<PlanDto>?> GetNotArchived(int userId);
    public Task Update(PlanUpdateDto request);
}
