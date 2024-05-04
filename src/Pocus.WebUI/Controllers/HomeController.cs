using Microsoft.AspNetCore.Mvc;

namespace Pocus.WebUI.Controllers
{
    public class HomeController : Controller
    {

        [HttpGet]
        public IActionResult MainPage()
        {
            return View("~/Views/Home/MainPage.cshtml");
        }

        [HttpGet]
        public IActionResult About()
        {
            return View("~/Views/Home/About.cshtml");
        }
    }
}
