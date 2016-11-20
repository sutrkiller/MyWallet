using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace MyWallet.Controllers
{
    public class AccountsController : Controller
    {
        public IActionResult AccessDenied()
        {
            return View();
        }
        
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.Authentication.SignOutAsync("Cookie");
            return RedirectToAction("Index", "Home");
        }
        
        public IActionResult GoogleSignIn(string returnUrl = "/")
        {
            return new ChallengeResult("Google", new AuthenticationProperties { RedirectUri = returnUrl });
        }
    }
}