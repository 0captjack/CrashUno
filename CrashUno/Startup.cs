using CrashUno.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrashUno
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<TrafficContext>(options =>
           {
               options.UseMySql(Configuration["ConnectionStrings:TrafficConnection"]);
           });

            services.AddDbContext<AppIdentityDBContext>(options => options.UseSqlite(Configuration["ConnectionStrings:IdentityConnection"]));

            services.AddScoped<IRepository, EFRepository>();

            services.AddRazorPages();

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDBContext>();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("admin", "admin", new { Controller = "Admin", action = "Index" });
                endpoints.MapControllerRoute("typepage",
                    "{crashseverityid}/Page{pageNum}",
                    new { Controller = "Home", action = "Crash" });

                endpoints.MapControllerRoute(
                    name: "Paging",
                    pattern: "Page{pageNum}",
                    defaults: new { Controller = "Home", action = "Index", pageNum = 1});

                endpoints.MapControllerRoute("type",
                    "{crashseverityid}",
                    new { Controller = "Home", action = "Crash", pageNum = 1 });
                
                endpoints.MapDefaultControllerRoute();

                //endpoints.MapRazorPages();
            });
            app.UseStaticFiles();

            IdentitySeedData.EnsurePopulated(app);
        }
    }
}
