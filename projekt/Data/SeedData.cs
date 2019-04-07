using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using projekt.Models;

namespace projekt.Data
{
    public static class SeedData
    {
        public static void PopulateWithRecipes(IApplicationBuilder app)
        {
            AppDbContext context = app.ApplicationServices.GetRequiredService<AppDbContext>();
            context.Database.Migrate();
            context.Recipes.RemoveRange(context.Recipes);

            /*if (!context.Recipes.Any())
            {
                context.Recipes.AddRange(
                    new Recipe
                    {
                        Name = "Lorem",
                        Body = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis nec pulvinar erat, id bibendum purus. Pellentesque sed viverra lorem, sed ultrices justo."
                    },
                    new Recipe
                    {
                        Name = "Ut felis enim",
                        Body = "Ut felis enim, maximus vitae dolor dictum, auctor iaculis nisi. Aenean non diam turpis. Nullam pharetra in eros ut condimentum. Cras nec mauris vehicula, dapibus dui a, placerat sem."
                    }
                );
                context.SaveChanges();
            }*/
        }

        public static void PopulateWithCategories(IApplicationBuilder app)
        {
            AppDbContext context = app.ApplicationServices.GetRequiredService<AppDbContext>();
            context.Database.Migrate();
            if (!context.Categories.Any())
            {
                context.Categories.AddRange(
                    new Category
                    {
                        Name = "Danie główne",
                    },
                    new Category
                    {
                        Name = "Zupa",
                    },
                    new Category
                    {
                        Name = "Deser",
                    },
                    new Category
                    {
                        Name = "Śniadanie",
                    },
                    new Category
                    {
                        Name = "Sałatka",
                    }
                );
            }
        }

        public static void PopulateWithDiffLevels(IApplicationBuilder app)
        {
            AppDbContext context = app.ApplicationServices.GetRequiredService<AppDbContext>();
            context.Database.Migrate();
            if (!context.DifficultyLevels.Any())
            {
                context.DifficultyLevels.AddRange(
                    new DifficultyLevel
                    {
                        Name = "Łatwe"
                    },
                    new DifficultyLevel
                    {
                        Name = "Średnie"
                    },
                    new DifficultyLevel
                    {
                        Name = "Trudne"
                    }
                );
            }
        }
    }
}
