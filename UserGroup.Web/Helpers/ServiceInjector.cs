using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using UserGroup.Common.Contracts;
using UserGroup.DAL;
using UserGroup.Services;

namespace UserGroup.Web.Helpers
{
    public static class ServiceInjector
    {
        public static void AddConfiguredService(IServiceCollection services)
        {
            ConfigureRepositories(services);
            ConfigureServiceLayer(services);
            ConfigureAutomapper(services);
        }

        private static void ConfigureRepositories(IServiceCollection services)
        {
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<ISearchRepository, SearchRepository>();
        }

        private static void ConfigureServiceLayer(IServiceCollection services)
        {
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<ISearchService, SearchService>();
        }

        private static void ConfigureAutomapper(IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}