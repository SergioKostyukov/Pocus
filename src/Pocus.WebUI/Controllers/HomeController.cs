using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Pocus.WebUI.Controllers
{
    [Authorize]
    public class HomeController(ILogger<HomeController> logger,
                                IHttpContextAccessor httpContextAccessor) : Controller
    {
        private readonly ILogger<HomeController> _logger = logger;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        [HttpGet]
        public IActionResult Index()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //_httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            //    ?? throw new InvalidOperationException("User not found");

            _logger.LogInformation(userId);

            return View();
        }

        [HttpGet]
        public IActionResult MainPage()
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? throw new InvalidOperationException("User not found");

            _logger.LogInformation(userId);
            return View("~/Views/Home/MainPage.cshtml");
        }
    }
}
