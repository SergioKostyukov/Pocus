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

    public async Task SetDefault(string userId)
    {
        try
        {
            await _dbContext.Settings
                .AddAsync(new Settings {
                    UserId = userId,
                    WorkTime = 30,
                    BreakTime = 10,
                    IsNotificationSound = true,
                    DayGoal = 3,
                    ResetTime = TimeSpan.Zero,
                    IgnoreWeekend = false,
                    ThemeColor = false,
                    IgnoreHabits = false,
                    BlockSites = false
                });

            await _dbContext.SaveChangesAsync();

            _logger.LogInformation($"Default settings are set");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Default setting error: {ex.Message}");
            throw;
        }
    }
    public async Task<SettingsDto> GetByUserId(string userId)
    {
        var settings = await _dbContext.Settings.FirstOrDefaultAsync(x => x.UserId == userId);

        return _mapper.Map<SettingsDto>(settings);
    }
    public async Task UpdateSessionParams(string userId, SessionSettingsDto request)
    {
        try
        {
            var settings = await _dbContext.Settings
              .Where(x => x.UserId == userId)
              .FirstAsync();

            settings.WorkTime = request.WorkTime;
            settings.BreakTime = request.BreakTime;
            settings.IsNotificationSound = request.IsNotificationSound;

            _dbContext.Settings.Update(settings);

            await _dbContext.SaveChangesAsync();

            _logger.LogInformation($"Session settings updated successful");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Update Session settings error: {ex.Message}");
            throw;
        }
    }
    public async Task UpdateGoalParams(string userId, GoalSettingsDto request)
    {
        try
        {
            var settings = await _dbContext.Settings
               .Where(x => x.UserId == userId)
               .FirstAsync();

            settings.DayGoal = request.DayGoal;
            settings.ResetTime = request.ResetTime;
            settings.IgnoreWeekend = request.IgnoreWeekend;

            _dbContext.Settings.Update(settings);

            await _dbContext.SaveChangesAsync();

            _logger.LogInformation("Goal settings updated successful");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Update Goal settings error: {ex.Message}");
            throw;
        }
    }
    public async Task UpdateOtherParams(string userId, OtherSettingsDto request)
    {
        try
        {
            var settings = await _dbContext.Settings
              .Where(x => x.UserId == userId)
              .FirstAsync();

            settings.ThemeColor = request.ThemeColor;
            settings.IgnoreHabits = request.IgnoreHabits;
            settings.BlockSites = request.BlockSites;

            _dbContext.Settings.Update(settings);

            await _dbContext.SaveChangesAsync();

            _logger.LogInformation($"Other settings updated successful");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Update Other settings error: {ex.Message}");
            throw;
        }
    }
}
