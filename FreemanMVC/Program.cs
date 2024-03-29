using FreemanMVC.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddMvc();
builder.Services.AddMemoryCache();
builder.Services.AddSession();
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection));
builder.Services.AddTransient<IProductRepository, EFProductRepository>();
builder.Services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<IOrderRepository, EFOrderRepository>();
var app = builder.Build();

app.UseSession();

app.UseDeveloperExceptionPage();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.MapControllerRoute(
    name: null,
    pattern: "{category}/Page{productPage:int}",
    defaults: new { controller = "Product", action = "List" });
app.MapControllerRoute(
    name: null,
    pattern: "Page{productPage:int}",
    defaults: new { controller = "Product", action = "List", productPage = 1 });
app.MapControllerRoute(
    name: null,
    pattern: "{category}",
    defaults: new { controller = "Product", action = "List", productPage = 1});
app.MapControllerRoute(
    name: null,
    pattern: "",
    defaults: new { controller = "Product", action = "List", productPage = 1 });
app.MapControllerRoute(
    name: null,
    pattern: "{controller}/{action}/{id?}");

SeedData.EnsurePopulaited(app);

app.UseAuthorization();

app.MapRazorPages();

app.Run();
