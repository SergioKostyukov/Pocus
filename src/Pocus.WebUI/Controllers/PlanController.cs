using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pocus.Application.Dto;
using Pocus.Application.Interfaces;
using Pocus.WebUI.Dto;
using Pocus.WebUI.Models;
using System.Security.Claims;

namespace Pocus.WebUI.Controllers
{
    [Authorize]
    public class PlanController(ILogger<PlanController> logger,
                                IHttpContextAccessor httpContextAccessor,
                                IPlanService planService,
                                ISettingsService settingsService) : Controller
    {
        private readonly ILogger<PlanController> _logger = logger;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly IPlanService _planService = planService;
        private readonly ISettingsService _settingsService = settingsService;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? throw new InvalidOperationException("User not found");

            var settings = await _settingsService.GetByUserId(userId);

            var plans = new List<PlanDto>();
            if (!settings.IgnoreHabits)
            {
                plans.Add(await _planService.GetHabits(userId));
            }
            plans.AddRange(await _planService.GetNotArchived(userId));

            var model = new PlanModel
            {
                Plans = plans
            };

            return View("~/Views/Plan/Plans.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> AddPlan([FromBody] NoteAddDto plan)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? throw new InvalidOperationException("User not found");

            try
            {
                plan.UserId = userId;

                await _planService.Create(plan);
                return Ok(new { message = "Plan successfully added" });
            }
            catch
            {
                return BadRequest(new { message = "Error adding Plan" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CopyPlan([FromBody] IdRequestDto request)
        {
            try
            {
                await _planService.Copy(request.Id);

                return Ok(new { message = "Plan copy successfully" });
            }
            catch
            {
                return BadRequest(new { message = "Plan copy failed" });
            }
        }

        [HttpPatch]
        public async Task<IActionResult> UpdatePlan([FromBody] PlanUpdateDto request)
        {
            try
            {
                await _planService.Update(request);

                return Ok(new { message = "Plan update successfully" });
            }
            catch
            {
                return BadRequest(new { message = "Plan update failed" });
            }
        }

        [HttpPatch]
        public async Task<IActionResult> UpdatePlanPin([FromBody] IdRequestDto request)
        {
            try
            {
                await _planService.UpdatePinnedStatus(request.Id);

                return Ok(new { message = "Plan pin update successfully" });
            }
            catch
            {
                return BadRequest(new { message = "Plan pin update failed" });
            }
        }

        [HttpPatch]
        public async Task<IActionResult> ArchivePlan([FromBody] IdRequestDto request)
        {
            try
            {
                await _planService.UpdateArchivedStatus(request.Id);

                return Ok(new { message = "Plan archive successfully" });
            }
            catch
            {
                return BadRequest(new { message = "Plan archive failed" });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePlan([FromBody] IdRequestDto request)
        {
            try
            {
                await _planService.Delete(request.Id);

                return Ok(new { message = "Plan deleted successfully" });
            }
            catch
            {
                return BadRequest(new { message = "Plan delete failed" });
            }
        }
    }
}
