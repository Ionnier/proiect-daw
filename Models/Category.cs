using System.ComponentModel.DataAnnotations;
using ProiectDAW.Models;

namespace ProiectDAWd.Models
{
    public class Category
    {
        public long CategoryId { get; set; }

        [Required]
        public string CategoryName { get; set; }

        [Required]
        public string CategoryDescription { get; set; }

        public List<Post> posts { get; set; } = new List<Post>();

    }
}
