using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetNote.Migrations;
using NetNote.Models;
using NetNote.ViewModels;
using NoteUser = NetNote.Models.NoteUser;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NetNote.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        public UserManager<NoteUser> UserManager { get; }
        public SignInManager<NoteUser> SignInManager { get; }
        public AccountController(UserManager<NoteUser> userManager,
            SignInManager<NoteUser> signInManager,
            ILogger<AccountController> logger)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            _logger = logger;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login(string returnUrl=null)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model,string returnUrl=null)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);
            if(result.Succeeded)
            {
                _logger.LogInformation("Logged in {userName}.", model.UserName);
                return RedirectToAction("Index", "Note");
            }
            else
            {
                _logger.LogWarning("Failed to log in {userName}.",model.UserName);
                ModelState.AddModelError("","用户名或密码错误");
                return View(model);
            }
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = new NoteUser { UserName = model.UserName, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if(result.Succeeded)
                {
                    _logger.LogInformation("User {userName} was created.", model.Email);
                    return RedirectToAction("Login");
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LogOff()
        {
            var userName = HttpContext.User.Identity.Name;
            await SignInManager.SignOutAsync();
            _logger.LogInformation("{userName} logged out.", userName);
            return RedirectToAction("Index", "Note");
        }
    }
}
