using Microsoft.AspNetCore.Mvc;
using Pocus.Application.Interfaces;
using Pocus.WebUI.Models;
using System.Security.Claims;
using Pocus.Application.Dto;

namespace Pocus.WebUI.Controllers
{
    public class SettingsController(ILogger<SettingsController> logger,
                                    IHttpContextAccessor httpContextAccessor,
                                    ISettingsService settingsService) : Controller
    {
        private readonly ILogger<SettingsController> _logger = logger;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly ISettingsService _settingsService = settingsService;

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? throw new InvalidOperationException("User not found");

            var settings = await _settingsService.GetByUserId(userId);

            var model = new SettingsModel
            {
                UserId = userId,
                SessionSettings = new SessionSettingsDto
                {
                    WorkTime = settings.WorkTime,
                    BreakTime = settings.BreakTime,
                    IsNotificationSound = settings.IsNotificationSound
                },
                GoalSettings = new GoalSettingsDto
                {
                    DayGoal = settings.DayGoal,
                    ResetTime = settings.ResetTime,
                    IgnoreWeekend = settings.IgnoreWeekend
                },
                OtherSettings = new OtherSettingsDto
                {
                    ThemeColor = settings.ThemeColor,
                    IgnoreHabits = settings.IgnoreHabits,
                    BlockSites = settings.BlockSites
                }
            };

            return View("~/Views/Settings/Settings.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSessionSettingsAsync(SettingsModel model)
        {
            await _settingsService.UpdateSessionParams(model.UserId, model.SessionSettings);

            return View("~/Views/Settings/Settings.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateGoalSettingsAsync(SettingsModel model)
        {
            await _settingsService.UpdateGoalParams(model.UserId, model.GoalSettings);

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateOtherSettingsAsync(SettingsModel model)
        {
            await _settingsService.UpdateOtherParams(model.UserId, model.OtherSettings);

            return View();
        }
    }
}
