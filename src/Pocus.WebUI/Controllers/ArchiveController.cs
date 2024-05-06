using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pocus.Application.Interfaces;
using Pocus.WebUI.Models;
using System.Security.Claims;

namespace Pocus.WebUI.Controllers
{
    [Authorize]
    public class ArchiveController(IHttpContextAccessor httpContextAccessor,
                                   IPlanService planService,
                                   INoteService noteService) : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly IPlanService _planService = planService;
        private readonly INoteService _noteService = noteService;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? throw new InvalidOperationException("User not found");

            var model = new ArchiveModel
            {
                PlansList = await _planService.GetArchived(userId),
                NotesList = await _noteService.GetArchived(userId)
            };

            return View("~/Views/Archive/Archive.cshtml", model);
        }
    }
}
