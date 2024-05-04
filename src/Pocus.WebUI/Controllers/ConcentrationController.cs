using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pocus.Application.Dto;
using Pocus.Application.Interfaces;
using System.Security.Claims;

namespace Pocus.WebUI.Controllers
{
    [Authorize]
    public class ConcentrationController(ILogger<ConcentrationController> logger,
                                IHttpContextAccessor httpContextAccessor) : Controller
    {
        private readonly ILogger<ConcentrationController> _logger = logger;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        [HttpGet]
        public IActionResult Get()
        {
            return View("~/Views/Concentration/Concentration.cshtml");
        }

        //[HttpPatch]
        //public async Task<IActionResult> UpdatePlanText([FromBody] ObjectUpdateTextDto request)
        //{
        //    try
        //    {
        //        await _planService.UpdateText(request);

        //        return Ok(new { message = "Plan test update successfully" });
        //    }
        //    catch
        //    {
        //        return BadRequest(new { message = "Plan test update failed" });
        //    }
        //}
    }
}
