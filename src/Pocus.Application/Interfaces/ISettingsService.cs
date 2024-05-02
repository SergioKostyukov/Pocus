using Pocus.Application.Dto;

namespace Pocus.Application.Interfaces;

public interface ISettingsService
{
    public Task SetDefault(int userId);
    public Task<SettingsDto> GetByUserId(int userId);
    public Task UpdateSessionParams(SessionSettingsDto request);
    public Task UpdateGoalParams(GoalSettingsDto request);
    public Task UpdateOtherParams(OtherSettingsDto request);
}
