using System;
using System.Configuration;
using FINALEXAM.DAL;
using FINALEXAM.Interfaces;
using FINALEXAM.Models;
using FINALEXAM.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
{
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequiredLength = 8;
    opt.Password.RequireDigit = true;
    opt.Password.RequireLowercase = true;
    opt.Password.RequireUppercase = true;
    opt.Password.RequiredUniqueChars = 1;

    opt.User.RequireUniqueEmail = true;

    opt.Lockout.AllowedForNewUsers = true;
    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(60);
    opt.Lockout.MaxFailedAccessAttempts = 5;

    opt.SignIn.RequireConfirmedEmail = true;
}).AddDefaultTokenProviders().AddEntityFrameworkStores<AppDBContext>();


builder.Services.AddDbContext<AppDBContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<LayoutService>();
builder.Services.AddScoped<IEmailService,EmailService>();
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("Email"));
builder.Services.AddTransient<IMailService,MailService>();


var app = builder.Build();





app.UseAuthentication();
app.UseAuthorization();

app.UseRouting();
app.UseStaticFiles();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.MapControllerRoute
    (
    name: "Default",
    pattern: "{controller=home}/{action=index}"
    );

app.Run();
