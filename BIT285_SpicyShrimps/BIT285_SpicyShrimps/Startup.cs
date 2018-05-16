using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BIT285_SpicyShrimps.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BIT285_SpicyShrimps
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
            //after we create the DbContext we need to Register the Context with dependency injection
            //Need to Register so can use Dependency Injection
            // next Line specifies we need the DBContext of type MathDbContext and within that
            //we need the Sql Server and pass the name of the connection string LibraryDemo1
            //Now we need to specify the connection string in the appsetting.json file
            services.AddDbContext<MathDbContext>(option => option.UseSqlServer(Configuration.GetConnectionString("DungenGems1")));
           
            services.AddMvc();
            services.AddDistributedMemoryCache();
            services.AddMvc()
                .AddSessionStateTempDataProvider();
            services.AddDbContext<MathDbContext>();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //below is added
            app.UseStaticFiles();

            //enable session before MVC
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
      
    }
}
