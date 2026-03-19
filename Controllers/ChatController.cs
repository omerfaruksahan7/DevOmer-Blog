using DevBlog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DevBlog.Controllers
{
    public class ChatController : Controller
    {
        private readonly AppDbContext _context;
        private readonly string _apiKey;

        // IConfiguration ile appsettings.json'dan API Key'i alıyoruz
        public ChatController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _apiKey = configuration.GetValue<string>("GeminiApiKey");
        }

        [HttpPost]
        public async Task<IActionResult> Sor([FromBody] ChatMesaj mesaj)
        {
            if (string.IsNullOrWhiteSpace(mesaj.Soru))
                return Json(new { yanit = "Lütfen bir soru sorun." });

            string prompt = "";

            // 1. KULLANICI BİR MAKALENİN İÇİNDEYSE (RAG Mantığı)
            if (mesaj.BlogId.HasValue)
            {
                var blog = _context.Blogs.FirstOrDefault(b => b.Id == mesaj.BlogId.Value);
                if (blog != null)
                {
                    string temizIcerik = System.Text.RegularExpressions.Regex.Replace(blog.Content, "<.*?>", string.Empty);

                    // DİKKAT: Talimatı kısalttık ve "EN FAZLA 2-3 CÜMLE" kuralını ekledik.
                    prompt = $"Sen 'DevOmer' blogunun yapay zeka asistanısın. Kullanıcı '{blog.Title}' adlı makaleyi okuyor. İçerik: '{temizIcerik}'. Kullanıcının sorusu: '{mesaj.Soru}'. Lütfen SADECE bu makaleye dayanarak, Türkçe ve ÇOK KISA bir cevap ver. Doğrudan sadede gel ve cevabın kesinlikle en fazla 2 veya 3 cümle olsun. Uzatma.";
                }
            }

            // 2. KULLANICI ANA SAYFADAYSA (Vitrin)
            if (string.IsNullOrEmpty(prompt))
            {
                // DİKKAT: Burada da kısalık kuralını ekledik.
                prompt = $"Sen 'DevOmer' blogunun yapay zeka asistanısın. Ziyaretçi sorusu: '{mesaj.Soru}'. Lütfen Türkçe, kibar ve doğrudan konuya giren ÇOK KISA (maksimum 1-2 cümle) bir cevap ver.";
            }

            // 3. GEMINI API'YE İSTEK AT
            string cevap = await GeminiApiYeSor(prompt);

            return Json(new { yanit = cevap });
        }

        // ARKA PLANDA GOOGLE İLE HABERLEŞEN METOT
        private async Task<string> GeminiApiYeSor(string prompt)
        {
            try
            {
                using var client = new HttpClient();

                // Gemini Flash modelinin resmi API adresi
                string url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent?key={_apiKey}";

                // Gemini'nin beklediği JSON yapısını oluşturuyoruz
                var requestBody = new
                {
                    contents = new[]
                    {
                        new { parts = new[] { new { text = prompt } } }
                    }
                };

                // İsteği paketle ve gönder
                var jsonContent = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(url, jsonContent);
                var responseString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    // Gelen karmaşık JSON'dan sadece cevabı (text) cımbızla çekiyoruz
                    using var jsonDoc = JsonDocument.Parse(responseString);
                    var text = jsonDoc.RootElement
                        .GetProperty("candidates")[0]
                        .GetProperty("content")
                        .GetProperty("parts")[0]
                        .GetProperty("text").GetString();

                    // Gemini Markdown dönebilir, alt satırları HTML <br> etiketine çevirip arayüze yolluyoruz
                    return text.Replace("\n", "<br>");
                }
                //else
                //{
                //    return "Yapay zeka servisi şu an meşgul, lütfen daha sonra tekrar deneyin.";
                //}
                //Gerçek hatayı ekrana basacak
                else
                {
                    // Google'ın bize gönderdiği GERÇEK hata mesajını yakalayıp ekrana basalım
                    string gercekHata = await response.Content.ReadAsStringAsync();
                    return $"Google'dan dönen hata: {gercekHata}";
                }
            }
            catch
            {
                return "Yapay zekaya bağlanırken bir hata oluştu.";
            }
        }
    }

    public class ChatMesaj
    {
        public string Soru { get; set; }
        public int? BlogId { get; set; }
    }
}