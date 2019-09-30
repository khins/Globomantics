using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Globomantics.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Globomantics
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            //services.AddSingleton<IConferenceService, ConferenceMemoryService>();
            //easily swap out the memory service with a real API service.
            //The controller doesn't have to change, and that's exactly the reason why using an interface is recommended.With an interface, 
            //the controller is decoupled from the actual implementation of the service class. 
            services.AddSingleton<IConferenceService, ConferenceApiService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //This method configures the HTTP request pipeline of ASP.NET Core. The pipeline specifies how the application should respond to HTTP requests.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //The important one is the StaticFiles middleware.If we don't add that, none of our images, CSS, and JavaScript files will be served to the browser. 
            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Conference}/{action=Index}/{id?}");
                
            });
        }
    }
}
