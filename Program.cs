using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProiectDAW.Context;
using ProiectDAW.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages(opts =>
{
	opts.Conventions.AuthorizeFolder("/Management", "AdminPolicy");
});

builder.Services.AddDbContext<ApplicationDbContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration.GetConnectionString("ProjectConnection"));
});

builder.Services.AddDbContext<IdentityContext>(opts => opts.UseSqlServer(
        builder.Configuration.GetConnectionString("ProjectConnection")
    )
);

builder.Services.AddAuthorization(opts =>
{
	opts.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<IdentityContext>();

builder.Services.Configure<IdentityOptions>(opts => {
	opts.Password.RequiredLength = 4;
	opts.Password.RequireNonAlphanumeric = false;
	opts.Password.RequireLowercase = false;
	opts.Password.RequireUppercase = false;
	opts.Password.RequireDigit = false;
	opts.User.RequireUniqueEmail = true;
	opts.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyz";
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

SeedData.EnsurePopulated(app);

app.Run();
