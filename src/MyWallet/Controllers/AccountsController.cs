using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyWallet.Models.Users;
using MyWallet.Services.Services.Interfaces;

namespace MyWallet.Controllers
{
    public class AccountsController : Controller
    {
        private readonly IUserService _userService;
        private readonly IEntryService _entryService;
        private readonly IMapper _mapper;

        public AccountsController(IUserService userService, IMapper mapper, IEntryService entryService)
        {
            _userService = userService;
            _mapper = mapper;
            _entryService = entryService;
        }

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

        [Authorize]
        public async Task<IActionResult> Manage(string message = null)
        {
            var user = await _userService.EnsureUserExists(User.Identity as ClaimsIdentity);
            if (user == null)
            {
                return NotFound();
            }
            var model = _mapper.Map<ManageUserCreateViewModel>(user);
            var currencies = await _entryService.GetAllCurrencies();
            var currenciesList = currencies.Select(x => new {x.Id, Value = x.Code});
            model.CurrenciesList = new SelectList(currenciesList,"Id","Value");
            return View("Manage",model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ManageUserCreateViewModel model)
        {
            await _userService.EditCurrency(model.Email, model.CurrencyId);
            TempData["Message"] = "Preferences changed";
            return RedirectToAction("Manage");
        }
    }
}