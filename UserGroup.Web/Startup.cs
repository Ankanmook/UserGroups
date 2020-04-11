using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UseGroup.DataModel.Models;
using Microsoft.Extensions.Configuration.Json;
using UserGroup.Common.Contracts;
using UserGroup.DAL;
using UserGroup.Services;
using AutoMapper;
using UserGroup.Common;

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
            ConfigureRepositories(services);
            ConfigureServiceLayer(services);
            ConfigureAutomapper(services);
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
            services.AddDbContext<PersonGroupsContext>(o => {
                o.UseSqlServer(connectionString);  
            });
        }

        public void ConfigureRepositories(IServiceCollection services)
        {
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<ISearchRepository, SearchRepository>();
        }

        public void ConfigureServiceLayer(IServiceCollection services)
        {
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<ISearchService, SearchService>();
        }

        public void ConfigureAutomapper(IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}
