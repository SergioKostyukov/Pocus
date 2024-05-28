using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pocus.WebUI.Models;
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

        //[HttpGet]
        //public async Task<IActionResult> GetByDay(StatisticModel model)
        //{

        //}

        //[HttpGet]
        //public async Task<IActionResult> GetByWeek(StatisticModel model)
        //{

        //}

        //[HttpGet]
        //public async Task<IActionResult> GetByMonth(StatisticModel model)
        //{

        //}

        //[HttpGet]
        //public async Task<IActionResult> GetByYear(StatisticModel model)
        //{

        //}
    }
}
