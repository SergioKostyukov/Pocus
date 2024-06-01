using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Pocus.Application.Dto;
using Pocus.Application.Interfaces;
using Pocus.Infrastructure.Data;

namespace Pocus.Application.Services;

internal class StatisticService(ILogger<StatisticService> logger,
                               PocusDbContext dbContext,
                               IMapper mapper) : IStatisticService
{
    private readonly ILogger<StatisticService> _logger = logger;
    private readonly PocusDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;

    public Task AddValue(AddStatisticDto request)
    {
        throw new NotImplementedException();
    }

    public async Task<List<StatisticViewDto>> GetStatistics(string userId, StatisticRangeType rangeType, int? day = null, int? month = null, int? year = null)
    {
        switch (rangeType)
        {
            case StatisticRangeType.Day:
                if (day.HasValue && month.HasValue && year.HasValue)
                {
                    return await GetByDay(userId, day.Value, month.Value, year.Value);
                }
                break;

            case StatisticRangeType.Week:
                if (day.HasValue && month.HasValue && year.HasValue)
                {
                    return await GetByWeek(userId, day.Value, month.Value, year.Value);
                }
                break;

            case StatisticRangeType.Month:
                if (month.HasValue && year.HasValue)
                {
                    return await GetByMonth(userId, month.Value, year.Value);
                }
                break;

            case StatisticRangeType.Year:
                if (year.HasValue)
                {
                    return await GetByYear(userId, year.Value);
                }
                break;

            default:
                throw new ArgumentException("Invalid range type");
        }

        throw new ArgumentException("Invalid arguments for the specified range type");
    }

    private async Task<List<StatisticViewDto>> GetByDay(string userId, int day, int month, int year)
    {
        var date = new DateTime(year, month, day);
        return await _dbContext.Statistic
            .Where(s => s.UserId == userId && s.Date.Date == date.Date)
            .Select(s => new StatisticViewDto
            {
                Date = s.Date,
                TimeComplete = s.TimeComplete,
                PlansComplete = s.PlansComplete,
                TimeProductivityByHours = s.TimeProductivityByHours,
                PlansProductivityByHours = s.PlansProductivityByHours
            })
            .ToListAsync();
    }

    public async Task<List<StatisticViewDto>> GetByWeek(string userId, int day, int month, int year)
    {
        var date = new DateTime(year, month, day);
        var startOfWeek = date.StartOfWeek(DayOfWeek.Monday);
        var endOfWeek = startOfWeek.AddDays(7);

        return await _dbContext.Statistic
            .Where(s => s.UserId == userId && s.Date >= startOfWeek && s.Date < endOfWeek)
            .Select(s => new StatisticViewDto
            {
                Date = s.Date,
                TimeComplete = s.TimeComplete,
                PlansComplete = s.PlansComplete,
                TimeProductivityByHours = s.TimeProductivityByHours,
                PlansProductivityByHours = s.PlansProductivityByHours
            })
            .ToListAsync();
    }

    public async Task<List<StatisticViewDto>> GetByMonth(string userId, int month, int year)
    {
        var startOfMonth = new DateTime(year, month, 1);
        var endOfMonth = startOfMonth.AddMonths(1);

        return await _dbContext.Statistic
            .Where(s => s.UserId == userId && s.Date >= startOfMonth && s.Date < endOfMonth)
            .Select(s => new StatisticViewDto
            {
                Date = s.Date,
                TimeComplete = s.TimeComplete,
                PlansComplete = s.PlansComplete,
                TimeProductivityByHours = s.TimeProductivityByHours,
                PlansProductivityByHours = s.PlansProductivityByHours
            })
            .ToListAsync();
    }

    public async Task<List<StatisticViewDto>> GetByYear(string userId, int year)
    {
        var startOfYear = new DateTime(year, 1, 1);
        var endOfYear = startOfYear.AddYears(1);

        return await _dbContext.Statistic
            .Where(s => s.UserId == userId && s.Date >= startOfYear && s.Date < endOfYear)
            .Select(s => new StatisticViewDto
            {
                Date = s.Date,
                TimeComplete = s.TimeComplete,
                PlansComplete = s.PlansComplete,
                TimeProductivityByHours = s.TimeProductivityByHours,
                PlansProductivityByHours = s.PlansProductivityByHours
            })
            .ToListAsync();
    }
}

public static class DateTimeExtensions
{
    public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
    {
        int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
        return dt.AddDays(-1 * diff).Date;
    }
}