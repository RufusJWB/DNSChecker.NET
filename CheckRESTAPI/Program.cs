using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace CheckRESTAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ////BuildWebHost(args).Run();

            // The ConfigureServices call here allows for ConfigureContainer to be supported in
            // Startup with a strongly-typed ContainerBuilder.
            var host = new WebHostBuilder()
                .UseKestrel()
                .ConfigureServices(services => services.AddAutofac())
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }

        ////public static IWebHost BuildWebHost(string[] args) =>
        ////    WebHost.CreateDefaultBuilder(args)
        ////        .UseStartup<Startup>()
        ////        .Build();
    }
}