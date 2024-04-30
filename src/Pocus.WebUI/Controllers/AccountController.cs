using Microsoft.AspNetCore.Mvc;
using Pocus.Identity;
using Pocus.WebUI.Models;
using Pocus.Identity.Dto;

namespace Pocus.WebUI.Controllers
{
    public class AccountController(ILogger<AccountController> logger,
                                   IIdentityService identityService) : Controller
    {
        private readonly ILogger<AccountController> _logger = logger;
        private readonly IIdentityService _identityService = identityService;

        [HttpGet]
        public IActionResult Login()
        {
            return View("~/Views/Identity/Login.cshtml");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View("~/Views/Identity/Register.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmLogin(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Identity/Login.cshtml", model);
            }

            await _identityService.LoginUser(new LoginDto
            {
                Tag = model.Tag,
                Password = model.Password
            });

            _logger.LogInformation("User login successfully");

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmRegister(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Identity/Register.cshtml", model);
            }

            await _identityService.RegisterUser(new RegisterDto
            {
                Tag = model.Tag,
                Email = model.Email,
                Password = model.Password
            });

            _logger.LogInformation("User register successfully");

            return RedirectToAction("Index", "Home");
        }
    }
}
