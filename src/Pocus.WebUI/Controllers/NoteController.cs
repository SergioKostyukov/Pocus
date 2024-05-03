using Microsoft.AspNetCore.Mvc;

namespace Pocus.WebUI.Controllers
{
    public class NoteController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return View("~/Views/Note/Note.cshtml");
        }
    }
}
