using Microsoft.AspNetCore.Mvc;

namespace Pocus.WebUI.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
