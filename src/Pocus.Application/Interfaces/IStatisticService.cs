using Pocus.Application.Dto;

namespace Pocus.Application.Interfaces;

public interface IStatisticService
{
    public Task AddValue(AddStatisticDto request);
    public Task<List<StatisticViewDto>> GetStatistics(string userId, StatisticRangeType rangeType, int? day = null, int? month = null, int? year = null);
}
