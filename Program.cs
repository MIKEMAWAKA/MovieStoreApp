
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MovieStoreApp.Models.Domain;
using MovieStoreApp.Repository.Abstract;
using MovieStoreApp.Repository.Implementation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IUserAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IFileService, FileService>();

builder.Services.AddScoped<IMovieService, MovieService>();

builder.Services.AddDbContext<MovieDatabaseContext>(options =>
  options.UseMySql(builder.Configuration.GetConnectionString("MoviestoreConnectionString"), ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("MoviestoreConnectionString")))

);

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<MovieDatabaseContext>()
    .AddDefaultTokenProviders();

//builder.Services.ConfigureApplicationCookie(options => options.LoginPath = "/UserAuthentication/Login");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

