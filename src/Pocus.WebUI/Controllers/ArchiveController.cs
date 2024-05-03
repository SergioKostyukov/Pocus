using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Pocus.WebUI.Controllers
{
    [Authorize]
    public class ArchiveController(ILogger<ArchiveController> logger,
                                IHttpContextAccessor httpContextAccessor) : Controller
    {
        private readonly ILogger<ArchiveController> _logger = logger;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        [HttpGet]
        public IActionResult Get()
        {
            return View("~/Views/Archive/Archive.cshtml");
        }
    }
}
