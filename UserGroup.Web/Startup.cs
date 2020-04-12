using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using UseGroup.DataModel.Models;
using UserGroup.Common;
using UserGroup.Web.Helpers;

namespace UserGroup.Web
{
    public class Startup
    {
        //public IConfiguration Configuration { get; }
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();

            services.AddRazorPages();

            services.AddControllersWithViews();

            services.AddMvc()
            .AddRazorPagesOptions(options =>
             {
                 options.Conventions.AddPageRoute("/Person/List", "");
             });

            ConfigureDbContext(services);
            ServiceInjector.Inject(services);
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseMvc();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            //(new StaticFileOptions()
            //{
            //    RequestPath = "/lib",
            //    FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "node_modules/"))
            //});

            app.UseNodeModules();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages()

                ;

                //endpoints.MapControllerRoute(
                //    name: "default",
                //    pattern: "{controller=Person}/{action=List}/");
                //endpoints.MapControllerRoute(
                //    name: "default",
                //    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        public void ConfigureDbContext(IServiceCollection services)
        {
            var connectionString = _configuration.GetConnectionString(Constants.DbConnectionString);
            services.AddDbContext<PersonGroupsContext>(o =>
            {
                o.UseSqlServer(connectionString);
            });
        }

    }
}