using DevBlog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DevBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly AppDbContext _context;
        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }


        public IActionResult Index()
        {
            // Sadece "Yayýnda" olan bloglarý tarihe göre en yeniler üstte olacak þekilde çekiyoruz
            var blogs = _context.Blogs
                .Include(b => b.Category)
                .Where(b => b.IsPublished)
                .OrderByDescending(b => b.CreatedDate)
                .ToList();

            return View(blogs);
        }

        // MAKALE OKUMA SAYFASI
        public IActionResult Oku(int id)
        {
            // Ýlgili blogu kategorisiyle birlikte buluyoruz
            var blog = _context.Blogs
                .Include(b => b.Category)
                .FirstOrDefault(b => b.Id == id);

            // Eðer blog yoksa veya yayýnda deðilse 404 sayfasýna gönder
            if (blog == null || !blog.IsPublished)
                return NotFound();

            // Okunma sayýsýný 1 artýr ve kaydet
            blog.ViewCount++;
            _context.SaveChanges();

            return View(blog); // Dolu blog nesnesini sayfaya gönder
        }



        public IActionResult Panel()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

