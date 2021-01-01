using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Hahn.ApplicatonProcess.December2020.Web
{
    #pragma warning disable CS1591
    public class Program
    {

        #pragma warning disable CS1591
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        #pragma warning disable CS1591
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging.AddFilter("Microsoft", LogLevel.Warning)
                    .AddFilter("System", LogLevel.Error);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseSerilog((hostingContext, loggerConfig) =>
                   loggerConfig.ReadFrom
                   .Configuration(hostingContext.Configuration)
                );
    }
}