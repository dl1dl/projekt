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

            if (!context.Recipes.Any())
            {
                context.Recipes.AddRange(
                    new Recipe
                    {
                        Name = "Lorem",
                        CategoryId = 1,
                        DiffLevelId = 1,
                        Body = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis nec pulvinar erat, id bibendum purus. Pellentesque sed viverra lorem, sed ultrices justo."
                    },
                    new Recipe
                    {
                        Name = "Ut felis enim",
                        CategoryId = 1,
                        DiffLevelId = 3,
                        Body = "Ut felis enim, maximus vitae dolor dictum, auctor iaculis nisi. Aenean non diam turpis. Nullam pharetra in eros ut condimentum. Cras nec mauris vehicula, dapibus dui a, placerat sem."
                    },
                    new Recipe
                    {
                        Name = "Integer",
                        CategoryId = 3,
                        DiffLevelId = 1,
                        Body = "Integer at tellus purus. Sed at urna est. Duis a leo sit amet massa tincidunt aliquam. Nulla eros est, fermentum vitae orci nec, scelerisque eleifend augue."
                    },
                    new Recipe
                    {
                        Name = "Sed vitae dapibus ante",
                        CategoryId = 2,
                        DiffLevelId = 2,
                        Body = "In leo libero, tristique quis magna ac, ultricies hendrerit enim. Duis rutrum erat sagittis est facilisis, ac malesuada mauris hendrerit. Morbi lacinia, ipsum quis convallis sollicitudin, ipsum dolor facilisis lacus, sit amet tempor mauris enim vitae diam. Ut rhoncus, eros quis varius ultricies, nisl nulla iaculis lorem, ut fermentum ex eros sit amet massa."
                    },
                    new Recipe
                    {
                        Name = "Curabitur volutpat",
                        CategoryId = 4,
                        DiffLevelId = 3,
                        Body = "Curabitur volutpat neque vitae dui efficitur, nec lobortis ex blandit. Mauris rutrum, arcu ut dignissim interdum, felis metus pharetra sem, et lacinia nulla massa id dui. "
                    },
                    new Recipe
                    {
                        Name = "Vivamus",
                        CategoryId = 5,
                        DiffLevelId = 1,
                        Body = "Vivamus malesuada, mi at fermentum efficitur, augue turpis luctus arcu, et hendrerit nisl massa fermentum est. Mauris at mauris diam. Vivamus fermentum, est id malesuada auctor, est mauris tempus eros, pretium vehicula elit lectus sit amet turpis."
                    }
                );
                context.SaveChanges();
            }
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
