using System.Reflection.Metadata;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProiectDAW.Context;

namespace ProiectDAW.Models
{
    public class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            ApplicationDbContext applicationDbContext = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();

            RoleManager<IdentityRole> roleManager = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            UserManager<IdentityUser> userManager = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            Random random1 = new Random();

            if (applicationDbContext.Database.GetPendingMigrations().Any())
            {
                applicationDbContext.Database.Migrate();
            }

            if (!applicationDbContext.categories.Any())
            {
                applicationDbContext.categories.AddRange(
                    createCategory("Sport", "Totul despre sport"),
                    createCategory("Auto", "Automobile si nu numai"),
                    createCategory("Stiinta", "Discutii despre fenomene stiintifice, teorii, teste, experimente, descoperiri, Brainiacs, astronomie, chimie, biologie... coltul oamenilor interesati si interesanti"),
                    createCategory("Traveling / Turism", "Calatorii, vacante, excurse, city break, drumetii, litoral, delta, munte, alpinism..."),
                    createCategory("Programming", "C & C++, .NET, PHP & MySQL, Delphi, Java, tutoriale... si toate celelalte domenii sunt aici !"),
                    createCategory("Jocuri & Console", "Totul despre jocuri pe PC sau console. Sfaturi, rezolvari la problemele voastre, strategii, noutati....etc.")
                );
                applicationDbContext.SaveChanges();
            }
            
            if (!applicationDbContext.posts.Any())
            {
                var categories = applicationDbContext.categories.ToList().Select(t => t.CategoryId).ToList();
                if (categories != null)
                { 
                    for (int i = 0; i < 6; i++)
                    {
						var asd = categories[random1.Next(0, categories.Count - 1)];
						applicationDbContext.posts.Add(
                            createPost($"Titlu{i}", $"Body{i}", categories[random1.Next(0, categories.Count - 1)])
                       );
                    }         
                    applicationDbContext.SaveChanges();
                }
                
            }

			if (!applicationDbContext.comments.Any())
			{
				var comments = applicationDbContext.posts.ToList().Select(t => t.PostId).ToList();
				if (comments != null)
				{

					
					for (int i = 0; i < 45; i++)
					{
						int asd;
						if (comments.Count > 1)
						{
							asd = comments[random1.Next(0, comments.Count - 1)];
						}
						else
						{
							asd = comments[0];
						}

						applicationDbContext.comments.Add(
                            createComment($"Comentariul{i}", asd)
					   );
					}
					applicationDbContext.SaveChanges();
				}

			}

			static ProiectDAWd.Models.Category createCategory(string name, string description)
            {
                return new ProiectDAWd.Models.Category
                {
                    CategoryName = name,
                    CategoryDescription = description,
                };
            }

            Post createPost(string title, string body, long categoryId)
            {
                return new Post
                {
                    PostTitle = title,
                    PostBody = body,
                    CreatedDate = RandomDay(),
                    CategoryId = categoryId
                };
            }

			Comment createComment(string body, int postId)
			{
				return new Comment
				{
                    CommentText = body,
                    PostId = postId,
					CreatedDate = RandomDay(),
				};
			}

			DateTime RandomDay()
            {
                DateTime start = new DateTime(1995, 1, 1);
                int range = (DateTime.Today - start).Days;
                return start.AddDays(random1.Next(range));
            }
            
            if (!roleManager.Roles.Any()) {
                roleManager.CreateAsync(
                    new IdentityRole("Admin") 
                ).GetAwaiter().GetResult();
            }

            if (!userManager.Users.Any())
            {
                var email = "admin@admin.ro";

                var result = userManager.CreateAsync(
					new IdentityUser
					{
						Email = email,
						UserName = "admin",
						EmailConfirmed = true,
					},
                    "admin"
                ).GetAwaiter().GetResult();
                var user =userManager.FindByEmailAsync(email).GetAwaiter().GetResult();
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").GetAwaiter().GetResult();
                    applicationDbContext.profiles.Add(
                        new Profile
                        {
                            UserId = user.Id,
                            UserName= "admin",
                        }
                    );
                    applicationDbContext.SaveChanges();
                }
            }
        
        }

    }
}
