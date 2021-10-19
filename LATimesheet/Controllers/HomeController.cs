using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LATimesheet.Models;
using Microsoft.AspNetCore.Identity;
using LATimesheet.Data.Entities;

namespace LATimesheet.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public HomeController(ILogger<HomeController> logger, SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginModel model)
        {
            if(!ModelState.IsValid) return View();
            
            try
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);
                
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Login failed, please check your details");
                    _logger.LogInformation($"{model.Username} Sign-In Failed");
                    return View();
                }

                _logger.LogInformation($"{model.Username} Sign-In Succeful");

                return Redirect("/dashboard");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                _logger.LogInformation(e.Message);
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> logout()
        {
            await _signInManager.SignOutAsync();
            return LocalRedirect("/");
        }
    }
}
