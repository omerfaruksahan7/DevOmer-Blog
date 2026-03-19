using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace DevBlog.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Kategori adı zorunludur.")]
        public string Name { get; set; }

        // Bir kategorinin birden fazla blogu olabilir
        public List<Blog> Blogs { get; set; } = new List<Blog>();
    }
}