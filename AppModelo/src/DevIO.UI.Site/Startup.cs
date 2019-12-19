using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DevIO.UI.Site
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<RazorViewEngineOptions>(option =>
            {
                option.AreaViewLocationFormats.Clear();
                option.AreaViewLocationFormats.Add("/Modulos/{2}/Views/{1}/{0}.cshtml");
                option.AreaViewLocationFormats.Add("/Modulos/{2}/Views/Shared/{0}.cshtml");
                option.AreaViewLocationFormats.Add("/Views/Shared/{0}.cshtml");
            });

            services.AddMvc(option =>
            {
                option.EnableEndpointRouting = false;                
            })
            .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {                
                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );

                routes.MapRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }
}
