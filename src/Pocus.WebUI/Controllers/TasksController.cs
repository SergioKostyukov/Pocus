using Microsoft.AspNetCore.Mvc;

namespace Pocus.WebUI.Controllers
{
    public class TasksController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
