using PHVManagementAGG.Core.DBAccess;
using PHVManagementAGG.Core.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;
using NetCore.AutoRegisterDi;
using PHVManagementAGG.Core.Services.Interfaces;
using PHVManagementAGG.Core.Services.Implementations;

namespace PHVManagementAGG.WebApp
{
    public static class DependencyConfig
    {
        public static void AddDependencies(this IServiceCollection services, IConfiguration configuration, string rootPath)
        {
            // configuration
            services.Configure<appSettings>(configuration.GetSection("appSettings"));
                        
            //services.Configure<MultipleDatabaseSettings>(configuration.GetSection(nameof(MultipleDatabaseSettings)));            
            services.Configure<appSettings>(configuration.GetSection("appSettings"));
            services.Configure<ShopeeSettings>(configuration.GetSection("ShopeeSettings"));
            
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.Load("PHVManagementAGG.Core"))
                .Where(c => c.Name.EndsWith("Services") || c.Name.EndsWith("Service"))
                .AsPublicImplementedInterfaces();

            services.AddScoped<ISqlDataAccess, SqlDataAccess>();
            services.AddScoped<ICommonService, CommonService>();


            services.AddTransient<IShopeeService, ShopeeService>();
            services.AddTransient<IGojekService, GojekService>();

        }
    }
}
