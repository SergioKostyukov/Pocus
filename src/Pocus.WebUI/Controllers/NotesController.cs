using Microsoft.AspNetCore.Mvc;

namespace Pocus.WebUI.Controllers
{
    public class NotesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
