using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Pocus.Application.Dto;
using Pocus.Application.Interfaces;
using Pocus.WebUI.Models;
using System.Security.Claims;

namespace Pocus.WebUI.Controllers
{
    [Authorize]
    public class ConcentrationController(ILogger<ConcentrationController> logger,
                                         IHttpContextAccessor httpContextAccessor,
                                         INoteService noteService,
                                         IPlanService planService,
                                         ISettingsService settingService) : Controller
    {
        private readonly ILogger<ConcentrationController> _logger = logger;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly INoteService _noteService = noteService;
        private readonly IPlanService _planService = planService;
        private readonly ISettingsService _settingService = settingService;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? throw new InvalidOperationException("User not found");

            var model = new ConcentrationModel
            {
                Plans = await _planService.GetTitlesOfNotArchived(userId),
                Notes = await _noteService.GetTitlesOfNotArchived(userId),
                Habits = await _planService.GetHabitsView(userId),
                Settings = await _settingService.GetByUserId(userId)
            };

            return View("~/Views/Concentration/Concentration.cshtml", model);
        }

        [HttpGet]
        public async Task<IActionResult> GetSessionActive(string plan, string? note, string? habits)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? throw new InvalidOperationException("User not found");

            var model = new SessionActiveModel
            {
                Plan = await _planService.GetById(int.Parse(plan)),
                Note = string.IsNullOrEmpty(note) ? null : await _noteService.GetById(int.Parse(note)),
                Habits = string.IsNullOrEmpty(habits) ? null : await _planService.GetHabitsView(userId),
                Settings = await _settingService.GetByUserId(userId)
            };

            return View("~/Views/Concentration/SessionActive.cshtml", model);
        }

        [HttpGet]
        public async Task<IActionResult> GetPlanById([FromQuery] int id)
        {
            PlanViewDto plan = await _planService.GetById(id);
            if (plan != null)
            {
                return Ok(new { message = "Plan data get successful", plan = plan });
            }
            else
            {
                return Ok(new { message = "There are no such Plan" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetNoteById([FromQuery] int id)
        {
            NoteViewDto note = await _noteService.GetById(id);
            if (note != null)
            {
                return Ok(new { message = "Note data get successful", note = note });
            }
            else
            {
                return Ok(new { message = "There are no such Note" });
            }
        }

        [HttpPatch]
        public async Task<IActionResult> UpdatePlanText([FromBody] ObjectUpdateTextDto request)
        {
            try
            {
                await _planService.UpdateText(request);

                return Ok(new { message = "Plan text update successfully" });
            }
            catch
            {
                return BadRequest(new { message = "Plan text update failed" });
            }
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateNoteText([FromBody] ObjectUpdateTextDto request)
        {
            try
            {
                await _noteService.UpdateText(request);

                return Ok(new { message = "Note text update successfully" });
            }
            catch
            {
                return BadRequest(new { message = "Note text update failed" });
            }
        }
    }
}
