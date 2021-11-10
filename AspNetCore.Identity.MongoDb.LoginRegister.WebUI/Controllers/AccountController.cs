using AspNetCore.Identity.MongoDb.LoginRegister.Models.Concrete;
using AspNetCore.Identity.MongoDb.LoginRegister.Models.RequestModels.Account;
using AspNetCore.Identity.MongoDbCore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AspNetCore.Identity.MongoDb.LoginRegister.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Personel> _userManager;
        private readonly SignInManager<Personel> _signInManager;
        private readonly RoleManager<MongoIdentityRole> _roleManager;

        public AccountController(UserManager<Personel> userManager,
            SignInManager<Personel> signInManager,
            RoleManager<MongoIdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterCreateModel m, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var user = new Personel
                {
                    UserName = m.UserName,
                    Email = m.Email
                };

                var result = await _userManager.CreateAsync(user, m.Password);
                if (result.Succeeded)
                {
                    var role = new MongoIdentityRole
                    {
                        Name = "admin",
                        NormalizedName = "ADMIN"
                    };


                    var resultRole = await _roleManager.CreateAsync(role);
                    await _userManager.AddToRoleAsync(user, "admin");

                    await _signInManager.SignInAsync(user, false);
                    return RedirectToLocal(returnUrl);
                }
            }
            return View(m);
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
                return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel m, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(m.UserName, m.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToLocal(returnUrl);
                }
            }
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
