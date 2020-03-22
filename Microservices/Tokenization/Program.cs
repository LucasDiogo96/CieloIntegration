using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.IO;
using Tokenization;

namespace CreditCard
{
    public class Tokenization
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateHostBuilder(string[] args) =>
         WebHost.CreateDefaultBuilder(args)
         .ConfigureAppConfiguration((host, config) => {

             var env = host.HostingEnvironment;
             var sharedFolder = Path.Combine(env.ContentRootPath, "..", "Shared");
             config
                 .AddJsonFile(Path.Combine(sharedFolder, "appsettings.json"), optional: true)
                 .AddJsonFile(Path.Combine(sharedFolder, $"appsettings.{env.EnvironmentName}.json"), optional: true);
             config.AddEnvironmentVariables();
         }).
         UseStartup<Startup>();
    }
}
