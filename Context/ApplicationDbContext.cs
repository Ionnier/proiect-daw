using Microsoft.EntityFrameworkCore;
using ProiectDAW.Models;
using ProiectDAWd.Models;

namespace ProiectDAW.Context
{
	public class ApplicationDbContext: DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

		public DbSet<Comment> comments { get; set; }
		public DbSet<Category> categories { get; set; }
		public DbSet<Post> posts { get; set; }	
		public DbSet<Profile> profiles { get; set; }	
	}
}
