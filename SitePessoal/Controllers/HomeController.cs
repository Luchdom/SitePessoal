using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Builder;

namespace SitePessoal.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStringLocalizer<HomeController> _localizer;
        private readonly IOptions<RequestLocalizationOptions> _locOptions;

        public HomeController(IStringLocalizer<HomeController> localizer, IOptions<RequestLocalizationOptions> locOptions)
        {
            _localizer = localizer;
            _locOptions = locOptions;
        }
        public IActionResult Index()
        {
            ViewBag.RequestCulture = Request.HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.DisplayName;
            ViewBag.CultureItems = _locOptions.Value.SupportedUICultures
                .Select(c => new SelectListItem { Value = c.Name, Text = c.DisplayName })
                .ToList();
            return View();
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
