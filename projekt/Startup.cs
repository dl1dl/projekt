﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using projekt.Models;
using projekt.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace projekt
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddTransient<IRecipeRepository, EFRecipeRepository>();
            services.AddTransient<ICatRepository, EFCatRepository>();
            services.AddTransient<IDiffLevelRepository, EFDiffLevelRepository>();

            services.AddDbContext<AppDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("AppDbContext")));

            services.AddIdentity<WebAppUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 5;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;

                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyz1234567890_-.@!$%#^&*()+=<>,/?";

            })
                    //.AddEntityFrameworkStores<AppIdentityDbContext>()
                    .AddEntityFrameworkStores<AppDbContext>()
                    .AddDefaultTokenProviders();

            /*services.AddDbContext<AppIdentityDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("IdentityContext")));*/
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "no-category",
                    template: "/",
                    defaults: new { controller = "Home", action = "Index", id = "0" });

                routes.MapRoute(
                    name: "category-danie-glowne",
                    template: "Kategoria/Danie-glowne",
                    defaults: new { controller = "Home", action = "Index", id = "6" });

                routes.MapRoute(
                    name: "category-zupa",
                    template: "Kategoria/Zupa",
                    defaults: new { controller = "Home", action = "Index", id = "7" });

               routes.MapRoute(
                    name: "category-deser",
                    template: "Kategoria/Deser",
                    defaults: new { controller = "Home", action = "Index", id = "8" });

                routes.MapRoute(
                    name: "category-sniadanie",
                    template: "Kategoria/Sniadanie",
                    defaults: new { controller = "Home", action = "Index", id = "9" });

                routes.MapRoute(
                    name: "category-salatka",
                    template: "Kategoria/Salatka",
                    defaults: new { controller = "Home", action = "Index", id = "10" });

                routes.MapRoute(
                    name: "category",
                    template: "Kategoria/{id?}",
                    defaults: new { controller = "Category", action = "AllRecipes" });

                routes.MapRoute(
                    name: "tag",
                    template: "Tag/{id?}",
                    defaults: new { controller = "Tag", action = "Index" });

                routes.MapRoute(
                    name: "recipe-details",
                    template: "Przepis/{id?}",
                    defaults: new { controller = "Recipe", action = "Details" });

                routes.MapRoute(
                    name: "author-details",
                    template: "Autor/{id?}",
                    defaults: new { controller = "Account", action = "Details" });

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            //SeedData.PopulateWithDiffLevels(app);
            //SeedData.PopulateWithRecipes(app);
            //AppIdentityDbContext.AddAdminWithRole(app.ApplicationServices, Configuration).Wait();
            AppDbContext.AddAdminWithRole(app.ApplicationServices, Configuration).Wait();
        }
    }
}
