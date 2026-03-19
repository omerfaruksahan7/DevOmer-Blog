using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevBlog.Models
{
    public class Blog
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Başlık zorunludur.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "İçerik zorunludur.")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Kapak görseli zorunludur.")]
        public string ImageUrl { get; set; }

        public bool IsPublished { get; set; } = true; // Herkese Açık Yayınla

        public int ViewCount { get; set; } = 0; // Okunma/Görüntülenme sayısı

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // Kategori İlişkisi
        [Required(ErrorMessage = "Lütfen bir kategori seçin.")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
    }
}