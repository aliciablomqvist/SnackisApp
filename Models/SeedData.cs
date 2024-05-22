using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SnackisApp.Data;
using SnackisApp.Models;
using System;
using System.Linq;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new ApplicationDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
        {
            // Kolla om det redan finns några kategorier.
            if (context.Category.Any())
            {
                Console.WriteLine("Categories already exist.");
                return;   // DB har redan seed-data.
            }

            Console.WriteLine("Seeding categories...");
            context.Category.AddRange(
                new Category
                {
                    Name = "Yoga"
                },
                new Category
                {
                    Name = "Meditation"
                },
                new Category
                {
                    Name = "Mindfulness"
                },
                new Category
                {
                    Name = "Lifestyle"
                },
                new Category
                {
                    Name = "Community"
                }
            );
            context.SaveChanges();
            Console.WriteLine("Categories seeded.");
        }
    }
}
