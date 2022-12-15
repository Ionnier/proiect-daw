using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProiectDAW.Models
{
	public class Profile
	{
		[Key]
		public string UserId { get; set; }

		[Required]
		public string UserName { get; set; }	

		public string? AvatarUri { get; set; }

		public string? Description { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public DateTime CreatedDate { get; set; }
 
		public List<Comment>? comments { get; set; }

		public List<Post>? posts { get; set; }


	}
}
