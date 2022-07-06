using ECommerce.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication("Identidade.Login")
                .AddCookie("Identidade.Login", config =>
                {
                    config.Cookie.Name = "Identidade.Login";
                    config.LoginPath = "/Login";
                    config.AccessDeniedPath = "/Home";
                    config.ExpireTimeSpan = TimeSpan.FromHours(1);
                });
builder.Services.AddDbContext<ECommerceDbContext>(options =>
options.UseSqlServer("Password=123456;Persist Security Info=True;User ID=sa;Initial Catalog=ECommerce;Data Source=DESKTOP-AKVL4PJ\\SQLEXPRESS"));

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
    pattern: "{controller=Usuario}/{action=Login}/{id?}");

app.Run();
