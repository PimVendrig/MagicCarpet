using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using MagicCarpetWebApp.Models;

namespace MagicCarpetWebApp
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
            services.AddMvc();

            services.AddDbContext<MagicCarpetWebAppContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("MagicCarpetWebAppContext")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            PrepareDatabase(app);

        }

        private void PrepareDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            using (var context = serviceScope.ServiceProvider.GetRequiredService<MagicCarpetWebAppContext>())
            {
                context.Database.Migrate();

                if (!context.ConcertLocations.Any())
                {
                    //Seed
                    var ziggoDome = new ConcertLocation { Name = "Ziggo Dome", Station = "ASB" };
                    var afasLive = new ConcertLocation { Name = "AFAS Live", Station = "ASB" };
                    var gelredome = new ConcertLocation { Name = "Gelredome", Station = "AH" };

                    context.ConcertLocations.Add(ziggoDome);
                    context.ConcertLocations.Add(afasLive);
                    context.ConcertLocations.Add(gelredome);

                    context.ConcertInfoes.Add(new ConcertInfo { Name = "Nicki Minaj", Location = ziggoDome, Date = new DateTime(2018, 08, 1, 22, 0, 0) });
                    context.ConcertInfoes.Add(new ConcertInfo { Name = "Nicki Minaj", Location = ziggoDome, Date = new DateTime(2018, 08, 2, 22, 0, 0) });
                    context.ConcertInfoes.Add(new ConcertInfo { Name = "Justin Bieber", Location = afasLive, Date = new DateTime(2018, 08, 3, 22, 0, 0) });
                    context.ConcertInfoes.Add(new ConcertInfo { Name = "Justin Bieber", Location = afasLive, Date = new DateTime(2018, 08, 4, 22, 0, 0) });
                    context.ConcertInfoes.Add(new ConcertInfo { Name = "Snow Patrol", Location = gelredome, Date = new DateTime(2018, 08, 5, 22, 0, 0) });

                    context.SaveChanges();
                }

            }
        }
    }
}
