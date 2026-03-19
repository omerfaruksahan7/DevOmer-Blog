using DevBlog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : AdminBaseController
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var blogs = _context.Blogs
                .Include(b => b.Category)
                .OrderByDescending(b => b.CreatedDate)
                .ToList();

            ViewBag.TotalBlogs = blogs.Count;
            ViewBag.TotalViews = blogs.Sum(b => b.ViewCount);
            return View(blogs);
        }


        // YENİ YAZI EKLE - SAYFAYI GÖSTERME (GET)
        public IActionResult YaziEkle()
        {
            // Veritabanında hiç kategori yoksa tasarımındaki kategorileri otomatik ekleyelim (Vakit kazandırır)
            if (!_context.Categories.Any())
            {
                _context.Categories.AddRange(
                    new Category { Name = "Yapay Zeka" },
                    new Category { Name = "SaaS" },
                    new Category { Name = "Proje Yönetimi" }
                );
                _context.SaveChanges();
            }

            // Kategorileri formdaki açılır liste (select) için sayfaya gönderiyoruz
            ViewBag.Categories = _context.Categories.ToList();
            return View();
        }

        // YENİ YAZI EKLE - FORMU KAYDETME (POST)
        [HttpPost]
        public IActionResult YaziEkle(Blog blog)
        {
            //Category nesnesinin boş olmasını hata olarak sayma!
            ModelState.Remove("Category");
            // Eğer formdaki bilgiler kurallara uygunsa (boş alan yoksa vs.) veritabanına kaydet
            if (ModelState.IsValid)
            {
                _context.Blogs.Add(blog);
                _context.SaveChanges();
                return RedirectToAction("Index"); // Kaydettikten sonra listeye geri dön
            }

            // Bir hata olursa kategorileri tekrar doldur ve hatalı formu geri göster
            ViewBag.Categories = _context.Categories.ToList();
            return View(blog);





        }
        // YAZI DÜZENLE - SAYFAYI DOLU GETİRME (GET)
        public IActionResult YaziDuzenle(int id)
        {
            // Tıklanan id'ye göre veritabanından blogu buluyoruz
            var blog = _context.Blogs.Find(id);
            if (blog == null) return NotFound(); // Eğer blog yoksa hata dön

            // Kategorileri yine sayfaya gönderiyoruz
            ViewBag.Categories = _context.Categories.ToList();

            // Bulduğumuz dolu blog nesnesini sayfaya yolluyoruz
            return View(blog);
        }

        // YAZI DÜZENLE - DEĞİŞİKLİKLERİ KAYDETME (POST)
        [HttpPost]
        public IActionResult YaziDuzenle(Blog blog)
        {
            //Category nesnesinin boş olmasını hata olarak sayma!
            ModelState.Remove("Category");
            if (ModelState.IsValid)
            {
                // Add yerine Update kullanıyoruz!
                _context.Blogs.Update(blog);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Categories = _context.Categories.ToList();
            return View(blog);
        }



        // YAZI SİL (GET)
        public IActionResult YaziSil(int id)
        {
            var blog = _context.Blogs.Find(id);
            if (blog != null)
            {
                _context.Blogs.Remove(blog);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
