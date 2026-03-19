using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http; // Session için gerekli

namespace DevBlog.Controllers
{
    // Korumasız, standart Controller. Herkes bu sayfaya girebilir.
    public class LoginController : Controller
    {
        // 1. GİRİŞ SAYFASINI EKRANA GETİR
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // 2. FORMDAN GELEN BİLGİLERİ KONTROL ET
        [HttpPost]
        public IActionResult Index(string username, string password)
        {
            // Şifre doğruysa Session'ı başlat ve Admin'e yolla
            if (username == "admin" && password == "12345")
            {
                HttpContext.Session.SetString("AdminGirisYaptiMi", "evet");
                return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
            }

            // Yanlışsa aynı sayfada hata mesajı göster
            ViewBag.Error = "Kullanıcı adı veya şifre hatalı!";
            return View();
        }

        // 3. ÇIKIŞ YAP VE SESSION'I SİL
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home"); // Çıkış yapınca ana sayfaya (vitrine) dön
        }

    }
}