using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using My101Romance.CustomAuthRoute;
using My101Romance.DAL;
using My101Romance.DAL.Interfaces;
using My101Romance.DAL.Repositories;
using My101Romance.Domain.Entity;
using My101Romance.Services;
using My101Romance.Services.Implementations;
using My101Romance.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();
builder.Services.AddServices();
builder.Services.AddRepository();
builder.Services.AddSession();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequiredLength = 4;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
})
    .AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddScoped<RoleManager<IdentityRole>>();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
});

builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();


builder.Configuration.Bind("Project", new Config());
builder.Configuration.Bind("SocialLinks", new Config());



var app = builder.Build();

// Настраиваем конвейер HTTP-запросов.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "quiz",
    pattern: "Quiz/SelectCard",
    defaults: new { controller = "Quiz", action = "SelectCard" });

app.MapControllerRoute(
    name: "login",
    pattern: "login",
    defaults: new { controller = "Account", action = "Login" });

app.MapControllerRoute(
    name: "register",
    pattern: "register",
    defaults: new { controller = "Account", action = "Register" });

app.MapControllerRoute(
    name: "quiz",
    pattern: "quiz",
    defaults: new { controller = "Quiz", action = "Play" });

app.MapControllerRoute(
    name: "top",
    pattern: "top",
    defaults: new { controller = "Card", action = "Top" },
    constraints: new { isAuthenticated = new IsNotAuth() });

app.MapControllerRoute(
    name: "random",
    pattern: "random",
    defaults: new { controller = "Card", action = "ShowRandomCards" });

app.MapControllerRoute(
    name: "new role",
    pattern: "admin/addrole",
    defaults: new { controller = "Admin", action = "CreateRole" });


app.MapControllerRoute(
    name: "logout",
    pattern: "Account/Logout",
    defaults: new { controller = "Account", action = "Logout" });


app.MapControllerRoute(
    name: "top18plus",
    pattern: "top",
    defaults: new { controller = "Card", action = "Top18Plus" },
    constraints: new { isAuthenticated = new IsAuth() });

app.Run();
