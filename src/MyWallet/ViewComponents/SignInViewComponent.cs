using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;

namespace MyWallet.ViewComponents
{
    public class SignInViewComponent : ViewComponent
    {
        private readonly HttpContext _httpContext;
        
        public SignInViewComponent(IHttpContextAccessor httpContextAccessor)
        {
            _httpContext = httpContextAccessor.HttpContext;
        }
        
        public Task<IViewComponentResult> InvokeAsync()
        {
            IViewComponentResult result;

            if (User.Identity.IsAuthenticated)
            {
                result = View("SignedIn", User.Identity.Name);
            }
            else
            {
                var returnUrl = _httpContext.Request.Query["returnUrl"].FirstOrDefault() ?? _httpContext.Request.GetDisplayUrl();
                result = View("SignedOut", returnUrl);
            }

            return Task.FromResult(result);
        }
    }
}
