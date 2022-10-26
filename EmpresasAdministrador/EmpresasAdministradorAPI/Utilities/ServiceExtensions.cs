using CompanyDomain.Repositories;
using CompanyDomain.Services;
using CompanyInfrastructure.Repositories;
using CompanyInfrastructure.Services;

namespace CompanyManagerAPI.Utilities
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterBusinessServices(this IServiceCollection services) 
        {
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<ICompanyService, CompanyService>();            

            services.AddScoped<IArticleRepository, ArticleRepository>();
            services.AddScoped<IArticleService, ArticleService>();

            services.AddScoped<IInventarioService, InventarioService>();
            return services;
        }
    }
}
