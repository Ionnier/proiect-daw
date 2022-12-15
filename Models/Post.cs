using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using ProiectDAWd.Models;

namespace ProiectDAW.Models
{
    public class Post
    {
        public int PostId { get; set; }

        [Required(ErrorMessage = "Titlul nu poate fi liber")]
        public string PostTitle { get; set; }

        [Required(ErrorMessage = "Continutul nu poate fi liber")]
        public string PostBody { get; set; } = string.Empty;

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedDate { get; set; }

        [ForeignKey("Category")]
        public long CategoryId { get; set; }

		public virtual Category? Category { get; set; }

        public List<Comment> comments { get; set; } = new List<Comment>();

		[ForeignKey("Profile")]
		public string? UserId { get; set; }

		public virtual Profile? User { get; set; }
	}
}
