using Microsoft.AspNetCore.Mvc;

namespace Pocus.WebUI.Controllers
{
    public class HomeController(ILogger<HomeController> logger) : Controller
	{
		private readonly ILogger<HomeController> _logger = logger;

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult MainPage()
		{
			return View("~/Views/Home/MainPage.cshtml");
		}
	}
}
