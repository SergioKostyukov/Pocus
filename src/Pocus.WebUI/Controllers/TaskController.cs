using Microsoft.AspNetCore.Mvc;

namespace Pocus.WebUI.Controllers
{
    public class TaskController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return View("~/Views/Task/Task.cshtml");
        }
    }
}
