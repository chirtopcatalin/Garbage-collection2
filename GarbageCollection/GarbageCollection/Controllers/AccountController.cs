using GarbageCollection.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using GarbageCollection.Models;

namespace GarbageCollection.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInMgr;
        public AccountController(SignInManager<IdentityUser> signInMgr)
        {
            _signInMgr = signInMgr;
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginModel { ReturnUrl = returnUrl });
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var result = await _signInMgr.PasswordSignInAsync(
                vm.Email, vm.Password, vm.RememberMe, false);

            if (result.Succeeded)
                return Redirect(vm.ReturnUrl ?? Url.Content("~/Admin/Index"));

            ModelState.AddModelError("", "Invalid login attempt");
            return View(vm);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInMgr.SignOutAsync();
            return RedirectToAction("Login");
        }

        public IActionResult AccessDenied() => View();
    }
}
