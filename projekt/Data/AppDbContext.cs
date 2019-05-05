using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using projekt.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace projekt.Data
{
    public class AppDbContext : IdentityDbContext<WebAppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<DifficultyLevel> DifficultyLevels { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public static async Task AddAdminWithRole(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            UserManager<WebAppUser> _userManager = serviceProvider.GetRequiredService<UserManager<WebAppUser>>();
            RoleManager<IdentityRole> _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string username = "admin@projekt.pl";
            string password = "admin1";
            string role = "Admin";

            if (await _roleManager.FindByNameAsync(role) == null)
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }

            if (await _userManager.FindByEmailAsync(username) == null)
            {
                WebAppUser admin = new WebAppUser
                {
                    UserName = username,
                    Email = username,
                };
                IdentityResult result = await _userManager.CreateAsync(admin, password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(admin, role);
                }
            }
        }
    }
}
