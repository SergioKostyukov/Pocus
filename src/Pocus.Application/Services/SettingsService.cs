using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Pocus.Application.Dto;
using Pocus.Application.Interfaces;
using Pocus.Core.Entities;
using Pocus.Infrastructure.Data;

namespace Pocus.Application.Services;

internal class SettingsService(ILogger<SettingsService> logger,
                               PocusDbContext dbContext,
                               IMapper mapper) : ISettingsService
{
    private readonly ILogger<SettingsService> _logger = logger;
    private readonly PocusDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;

    public async Task SetDefault(int userId)
    {
        try
        {
            await _dbContext.Settings
                .AddAsync(new Settings { UserId = userId });

            _logger.LogInformation($"Default settings are set");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Default setting error: {ex.Message}");
            throw;
        }
    }
    public async Task<SettingsDto> GetByUserId(int userId)
    {
        var settings = await _dbContext.Settings.Where(x => x.UserId == userId).FirstAsync();

        return _mapper.Map<SettingsDto>(settings);
    }
    public async Task UpdateSessionParams(SessionSettingsDto request)
    {
        try
        {
            var settings = await _dbContext.Settings
              .Where(x => x.UserId == request.UserId)
              .FirstAsync();

            settings.WorkTime = request.WorkTime;
            settings.BreakTime = request.BreakTime;
            settings.IsNotificationSound = request.IsNotificationSound;

            _dbContext.Settings.Update(settings);

            await _dbContext.SaveChangesAsync();

            _logger.LogInformation($"Other settings updated successful");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Update other settings error: {ex.Message}");
            throw;
        }
    }
    public async Task UpdateGoalParams(GoalSettingsDto request)
    {
        try
        {
            var settings = await _dbContext.Settings
               .Where(x => x.UserId == request.UserId)
               .FirstAsync();

            settings.DayGoal = request.DayGoal;
            settings.ResetTime = request.ResetTime;
            settings.IgnoreWeekend = request.IgnoreWeekend;

            _dbContext.Settings.Update(settings);

            await _dbContext.SaveChangesAsync();

            _logger.LogInformation("Session settings updated successful");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Update session settings error: {ex.Message}");
            throw;
        }
    }
    public async Task UpdateOtherParams(OtherSettingsDto request)
    {
        try
        {
            var settings = await _dbContext.Settings
              .Where(x => x.UserId == request.UserId)
              .FirstAsync();

            settings.ThemeColor = request.ThemeColor;
            settings.IgnoreHabits = request.IgnoreHabits;
            settings.BlockSites = request.BlockSites;

            _dbContext.Settings.Update(settings);

            await _dbContext.SaveChangesAsync();

            _logger.LogInformation($"Goal settings updated successful");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Update goal settings error: {ex.Message}");
            throw;
        }
    }
}
