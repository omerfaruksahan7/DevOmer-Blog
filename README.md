# DevOmer - Yapay Zeka (Gemini AI) Destekli Akıllı Blog Platformu 🚀

[![.NET Version](https://img.shields.io/badge/.NET-8.0-purple.svg)](https://dotnet.microsoft.com/download/dotnet/8.0)
[![AI Model](https://img.shields.io/badge/AI-Gemini%202.5%20Flash-blue.svg)](https://ai.google.dev/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

DevOmer Blog, ASP.NET Core MVC mimarisi üzerine inşa edilmiş, sıradan blog platformlarından farklı olarak ziyaretçilerine okudukları içerik hakkında **bağlama duyarlı (context-aware)** yapay zeka asistanı sunan modern bir kişisel blog projesidir.

---

## 📸 Ekran Görüntüleri & Demo

Projenin arayüzüne ve temel özelliklerine hızlıca göz atın:

### 1. Ziyaretçi Ana Sayfası & Arama
Statik HTML tasarımdan dinamik yapıya geçiş. Kategori hapları (pills) ve sayfa yenilenmeden çalışan AJAX tabanlı anlık arama motoru.

<img width="1283" height="778" alt="image" src="https://github.com/user-attachments/assets/8c17c34a-4a88-44ee-8160-a1a4e722d481" />

### 2. Akıllı Makale Asistanı (RAG Mantığı)
Sadece `Oku.cshtml` sayfasında görünen, ziyaretçinin okuduğu makalenin içeriğini anlayan ve sorularına o makale temelinde kısa, öz cevaplar veren **Gemini 2.5 Flash** entegrasyonlu chatbot.

<img width="1280" height="830" alt="image" src="https://github.com/user-attachments/assets/ba27676e-4ed1-4eda-b9ad-0d2a97522649" />

### 3. Yönetim (Admin) Paneli
Güvenli giriş, zengin metin editörü (Quill.js) ile makale oluşturma, kategori yönetimi ve yayına alma süreçlerinin yönetildiği panel.

<img width="1457" height="750" alt="image" src="https://github.com/user-attachments/assets/8fc41944-d794-43e6-8233-369fc762301f" />

---

## 🌟 Öne Çıkan Özellikler

- **🤖 Akıllı Makale Analizi (RAG):** Chatbot, genel AI cevapları yerine sadece o an okunan makale içeriğine dayanarak cevap verir. Halüsinasyonu (uydurmayı) engeller.
- **🔍 Gelişmiş Filtreleme:** Kategori bazlı filtreleme ve makale başlıklarında anlık arama (JS/AJAX).
- **📝 Zengin Metin Editörü:** Quill.js entegrasyonu ile Admin panelinde HTML formatlı profesyonel makale yazımı.
- **🔐 Güvenli Yönetim:** Session tabanlı kullanıcı oturumu ve şifreli Admin girişi.
- **📱 Responsive Tasarım:** Bootstrap 5 ile tüm cihazlarda (Mobil, Tablet, Masaüstü) kusursuz görünüm.

---

## 🛠️ Kullanılan Teknolojiler

| Alan | Teknoloji | Açıklama |
| :--- | :--- | :--- |
| **Backend** | C#, .NET 8.0 | ASP.NET Core MVC Mimarisi |
| **Yapay Zeka** | Google Gemini 2.5 Flash | HttpClient ile API Entegrasyonu |
| **Veritabanı** | MS SQL Server | Entity Framework Core (Code-First) |
| **Frontend** | Bootstrap 5, Vanilla JS | AJAX, Quill.js, FontAwesome |

---

## ⚙️ Kurulum ve Çalıştırma

Projeyi kendi bilgisayarınızda çalıştırmak için şu adımları izleyin:

### 1. Ön Hazırlıklar
* Bilgisayarınızda [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) yüklü olmalıdır.
* MS SQL Server (veya LocalDB) kurulu olmalıdır.
* Ücretsiz bir [Google Gemini API Key](https://aistudio.google.com/) almalısınız.

### 2. Projeyi Klonlayın
```bash
git clone [https://github.com/KULLANICI_ADIN/DevOmer-Blog.git](https://github.com/KULLANICI_ADIN/DevOmer-Blog.git)
cd DevOmer-Blog
