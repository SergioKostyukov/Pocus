using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Pocus.Application.Dto;
using Pocus.Application.Interfaces;
using Pocus.Core.Entities;
using Pocus.Infrastructure.Data;

namespace Pocus.Application.Services;

internal class PlanService(ILogger<PlanService> logger,
                           PocusDbContext dbContext,
                           IMapper mapper) : IObjectService, IPlanService
{
    private readonly ILogger<PlanService> _logger = logger;
    private readonly PocusDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;

    public async Task CreateDefault(string userId)
    {
        await _dbContext.Plans.AddAsync(new Plan
        {
            UserId = userId,
            Title = "Habits",
            Text = "[{\"text\":\"Planing\",\"isDone\":false}," +
                   "{\"text\":\"Writing day note\",\"isDone\":false}," +
                   "{\"text\":\"Reading\",\"isDone\":false}]",
            IsArchived = true,
            IsPinned = false,
        });

        await _dbContext.SaveChangesAsync();
    }
    public async Task Create(PlanDto request)
    {
        try
        {
            await _dbContext.Plans.AddAsync(_mapper.Map<Plan>(request));
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation($"New Plan created");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error adding Plan: {ex.Message}");
            throw;
        }
    }
    public async Task Copy(int objectId)
    {
        try
        {
            var planToCopy = await _dbContext.Plans
                .Where(x => x.Id == objectId)
                .FirstOrDefaultAsync();

            if (planToCopy != null)
            {
                _dbContext.Plans.Add(new Plan
                {
                    UserId = planToCopy.UserId,
                    Title = planToCopy.Title,
                    Text = planToCopy.Text,
                    IsArchived = planToCopy.IsArchived,
                    IsPinned = planToCopy.IsPinned
                });

                await _dbContext.SaveChangesAsync();

                _logger.LogInformation($"Plan copied");
            }
            else
            {
                _logger.LogError($"Plan with id {objectId} not found.");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error Plan copy: {ex.Message}");
            throw;
        }
    }
    public async Task<PlanDto?> GetHabits(string userId)
    {
        var userHabits = await _dbContext.Plans
            .Where(x => x.UserId == userId && x.Title == "Habits")
            .FirstAsync();

        return _mapper.Map<PlanDto>(userHabits);
    }
    public async Task<PlanViewDto?> GetById(int planId)
    {
        var plan = await _dbContext.Plans
            .Where(x => x.Id == planId)
            .FirstAsync();

        return _mapper.Map<PlanViewDto>(plan);
    }
    public async Task<List<PlanDto>?> GetArchived(string userId)
    {
        var userPlans = await _dbContext.Plans
            .Where(x => x.UserId == userId &&
                   x.IsArchived &&
                   x.Title != "Habits")
            .ToListAsync();

        return _mapper.Map<List<PlanDto>>(userPlans);
    }
    public async Task<List<PlanDto>?> GetNotArchived(string userId)
    {
        var userPlans = await _dbContext.Plans
            .Where(x => x.UserId == userId &&
                        !x.IsArchived &&
                        x.Title != "Habits")
            .ToListAsync();

        return _mapper.Map<List<PlanDto>>(userPlans);
    }
    public async Task<List<ObjectTitleDto>?> GetTitlesOfNotArchived(string userId)
    {
        var userPlans = await _dbContext.Plans
            .Where(x => x.UserId == userId &&
                        !x.IsArchived)
            .ToListAsync();

        return _mapper.Map<List<ObjectTitleDto>>(userPlans);
    }
    public async Task Update(PlanUpdateDto request)
    {
        try
        {
            var plan = await _dbContext.Plans
                .Where(x => x.Id == request.Id)
                .FirstAsync();

            plan.Title = request.Title;
            plan.Text = request.Text;
            plan.NotificationTime = request.NotificationTime;
            plan.IsPinned = request.IsPinned;

            _dbContext.Plans.Update(plan);

            await _dbContext.SaveChangesAsync();

            _logger.LogInformation($"Plan data updated");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error Plan data update: {ex.Message}");
            throw;
        }
    }
    public async Task UpdateText(ObjectUpdateTextDto request)
    {
        try
        {
            var plan = await _dbContext.Plans
                .Where(x => x.Id == request.Id)
                .FirstAsync();

            plan.Text = request.Text;

            _dbContext.Plans.Update(plan);

            await _dbContext.SaveChangesAsync();

            _logger.LogInformation($"Plan data updated");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error Plan data update: {ex.Message}");
            throw;
        }
    }
    public async Task UpdatePinnedStatus(int objectId)
    {
        try
        {
            var plan = await _dbContext.Plans
                .Where(x => x.Id == objectId)
                .FirstAsync();

            plan.IsPinned = !plan.IsPinned;

            _dbContext.Plans.Update(plan);

            await _dbContext.SaveChangesAsync();

            _logger.LogInformation($"Plan pin status updated");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error Plan pin status update: {ex.Message}");
            throw;
        }
    }
    public async Task UpdateArchivedStatus(int objectId)
    {
        try
        {
            var plan = await _dbContext.Plans
                            .Where(x => x.Id == objectId)
                            .FirstAsync();

            plan.IsArchived = !plan.IsArchived;

            _dbContext.Plans.Update(plan);

            await _dbContext.SaveChangesAsync();

            _logger.LogInformation($"Plan archived");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error Plan archive: {ex.Message}");
            throw;
        }
    }
    public async Task Delete(int objectId)
    {
        try
        {
            var itemToDelete = await _dbContext.Plans
                .FindAsync(objectId) ?? throw new InvalidOperationException("Plan not found");

            _dbContext.Plans.Remove(itemToDelete);
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation($"Plan deleted");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error Plan delete: {ex.Message}");
        }
    }
}
