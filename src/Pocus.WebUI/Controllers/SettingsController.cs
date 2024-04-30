using Microsoft.AspNetCore.Mvc;

namespace Pocus.WebUI.Controllers
{
    public class SettingsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
