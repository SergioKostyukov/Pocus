using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Pocus.WebUI.Controllers
{
    [Authorize]
    public class StatisticController(ILogger<StatisticController> logger,
                                IHttpContextAccessor httpContextAccessor) : Controller
    {
        private readonly ILogger<StatisticController> _logger = logger;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        [HttpGet]
        public IActionResult Get()
        {
            return View("~/Views/Statistic/Statistic.cshtml");
        }

    }
}
