using FreemanMVC.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddMvc();
string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection));
builder.Services.AddTransient<IProductRepository, EFProductRepository>();
var app = builder.Build();

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
