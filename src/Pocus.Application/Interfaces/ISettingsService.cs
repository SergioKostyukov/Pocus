using Pocus.Application.Dto;

namespace Pocus.Application.Interfaces;

public interface ISettingsService
{
    public Task SetDefault(string userId);
    public Task<SettingsDto> GetByUserId(string userId);
    public Task UpdateSessionParams(string userId, SessionSettingsDto request);
    public Task UpdateGoalParams(string userId, GoalSettingsDto request);
    public Task UpdateOtherParams(string userId, OtherSettingsDto request);
}
