﻿using Microsoft.EntityFrameworkCore;
namespace FreemanMVC.Models
{
    public static class SeedData
    {
        public static void EnsurePopulaited(IApplicationBuilder app)
        {
            ApplicationDbContext context = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();
            if(!context.Products.Any())
            {
                context.Products.AddRange(
                    new Product { Name = "Kayak", Description = "A boat for one person", Category = "Watersports", Price = 275 },
                    new Product { Name = "Lifejacket", Description = "Protective and fashionable", Category = "Watersports", Price = 48},
                    new Product { Name = "Soccer Ball", Description = "FIFA-approved size and weight", Category = "Soccer", Price = 19 },
                    new Product { Name = "Corner Flags", Description = "Give your plaing field a professional touch", Category = "Soccer", Price = 34 },
                    new Product { Name = "Stadium", Description = "Flat-packed 35,000-seat stadium", Category = "Soccer", Price = 79000 },
                    new Product { Name = "Thinking Cap", Description = "Improve brain efficiency by 75%", Category = "Chess", Price = 16 },
                    new Product { Name = "Unsteady Chair", Description = "Secretly give your opponent a disadvantege", Category = "Chess", Price = 29 },
                    new Product { Name = "Human Chess Board", Description = "A fun game for the family", Category = "Chess", Price = 75 },
                    new Product { Name = "Bling-Bling King", Description = "Gold-plated, diamond-studded King", Category = "Chess", Price = 1200 }
                    );
                context.SaveChanges();
            }
        }
    }
}
