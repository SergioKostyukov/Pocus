using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Pocus.WebUI.Controllers
{
    [Authorize]
    public class ConcentrationController(ILogger<ConcentrationController> logger,
                                IHttpContextAccessor httpContextAccessor) : Controller
    {
        private readonly ILogger<ConcentrationController> _logger = logger;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        [HttpGet]
        public IActionResult Get()
        {
            return View("~/Views/Concentration/Concentration.cshtml");
        }
    }
}
