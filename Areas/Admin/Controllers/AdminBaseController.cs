using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DevBlog.Areas.Admin.Controllers
{
    // MVC'nin hazır Controller sınıfından kalıtım alıyoruz
    public class AdminBaseController : Controller
    {
        // Sayfa çalışmadan hemen önce araya giren o meşhur metot
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var oturum = HttpContext.Session.GetString("AdminGirisYaptiMi");

            // Eğer Session boşsa (giriş yapılmamışsa) Login sayfasına gönder
            if (string.IsNullOrEmpty(oturum))
            {
                context.Result = new RedirectToActionResult("Index", "Login", new { area = "" });
            }

            base.OnActionExecuting(context);
        }
    }
}