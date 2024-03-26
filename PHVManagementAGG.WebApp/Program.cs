using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;


namespace PHVManagementAGG.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;                
            }
            host.Run();
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                    .ConfigureAppConfiguration(SetupConfiguration);
                });

        private static void SetupConfiguration(WebHostBuilderContext hostingContext, IConfigurationBuilder configBuilder)
        {
            var env = hostingContext.HostingEnvironment;

            configBuilder.SetBasePath(env.ContentRootPath)
                .AddEnvironmentVariables();

            configBuilder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
            configBuilder.AddEnvironmentVariables();

            configBuilder.Build();
        }
    }
}
