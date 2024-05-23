using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Pocus.Application.Dto;
using Pocus.Application.Interfaces;
using Pocus.Core.Entities;
using Pocus.Infrastructure.Data;

namespace Pocus.Application.Services;

internal class StatisticService(ILogger<StatisticService> logger,
                               PocusDbContext dbContext,
                               IMapper mapper) : IStatisticService
{
    private readonly ILogger<StatisticService> _logger = logger;
    private readonly PocusDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;

    
}
