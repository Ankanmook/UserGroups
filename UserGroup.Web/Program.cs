using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog.Web;
using System;
using UseGroup.DataModel.Models;

namespace UserGroup.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLogBuilder
                        .ConfigureNLog("nlog.config")
                        .GetCurrentClassLogger();

            try
            {
                logger.Info("Initializing application...");
                var host = CreateHostBuilder(args).Build();

                //TODO: remove this
                //using(var scope = host.Services.CreateScope())
                //{
                //    try
                //    {
                //        var context = scope.ServiceProvider.GetService<PersonGroupsContext>();
                //        context.Database.EnsureCreated();
                //        context.Database.Migrate();
                //    }
                //}

                //run the web app
                host.Run();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Application stopped bacuase of excetion");
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                    .UseNLog();
                });
    }
}