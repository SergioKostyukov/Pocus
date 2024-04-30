using Microsoft.AspNetCore.Mvc;

namespace Pocus.WebUI.Models
{
    public class AccountModel : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
