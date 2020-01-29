using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChessOnline.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ChessOnline
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
            services.AddControllersWithViews();

           // services.Configure<CookiePolicyOptions>(options =>
           // {
           //     // This lambda determines whether user consent for non-essential cookies is needed for a given request.
           ////     options.CheckConsentNeeded = context => true;
           //     options.MinimumSameSitePolicy = SameSiteMode.None;
           // });// Configure Policy for cookies 

            services.AddMvc();//add and set model view control (mvc) version
            services.AddTransient<LogInMiddleware>();//add LogInMiddleware
        } // add services at startup

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
            // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.


            // This method gets called by the runtime. Use this method to add services to the container.


            // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
            app.UseStaticFiles();//Enables static file serving for the current request path
            app.UseCookiePolicy();
            app.UseMiddleware<LogInMiddleware>();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=LogIn}/{id?}");
            });
        }
    }
}

