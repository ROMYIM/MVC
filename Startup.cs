using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MVC.Data;
using MVC.Extensions;
using MVC.Tasks;

namespace MVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            Configuration = configuration;
            LoggerFactory = loggerFactory;
        }
        public IConfiguration Configuration { get; }
        public ILoggerFactory LoggerFactory { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedMemoryCache();
            services.AddSession(options => 
            {
            });
            services.AddDbContext<QrCoreContext>(optionsBuilder => 
                optionsBuilder.UseMySQL(Configuration.GetConnectionString("default")));
            // services.AddHangfire(c => c.UseMemoryStorage());
            services.AddTimedJob();
            services.AddMvc();
            // services.AddScoped<IntervalTask>();
            // services.AddSingleton<IntervalTask>(new IntervalTask(new QrCoreContext(Configuration.GetConnectionString("default")), LoggerFactory));
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
            }

            // app.UseHangfireServer();

            // app.UseHangfireDashboard();

            app.UseTimedJob();

            // app.UseMiddleware<IntervalTask>();

            app.UseStaticFiles();

            app.UseSession();

            // app.UseLogin();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
