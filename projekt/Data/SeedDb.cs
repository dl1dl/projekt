using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using projekt.Models;

namespace projekt.Data
{
    public class SeedDb
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Categories.Any())
            {
                return;
            }

            var cat = new Category[]
                {
                    new Category { Name = "Danie główne"},
                    new Category { Name = "Zupa"},
                    new Category { Name = "Deser"},
                    new Category { Name = "Śniadanie"},
                    new Category { Name = "Sałatka"}
                };

            foreach (Category c in cat)
            {
                context.Categories.Add(c);
            }

            context.SaveChanges();
        }
    }
}
