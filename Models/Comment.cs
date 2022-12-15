using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using ProiectDAWd.Models;

namespace ProiectDAW.Models
{
	public class Comment
	{
		[Required]
		public long CommentId { get; set; }

		[Required(AllowEmptyStrings = false, ErrorMessage = "Un comentariu nu poate sa fie null.")]
		public string CommentText { get; set; } = string.Empty;

		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public DateTime CreatedDate { get; set; } = DateTime.Now;

		[ForeignKey("Post")]
		public int PostId { get; set; }

		public virtual Post? Post { get; set; }

		[ForeignKey("Profile")]
		public string? UserId { get; set; }

		public virtual Profile? User { get; set; }
	}
}
