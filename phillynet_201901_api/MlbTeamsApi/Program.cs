using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace MlbTeamsApi
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public static class Program
    {
        public static void Main(string[] args)
        {
            // Launch the web app
            BuildWebHost(args).Run();
        }

        private static IWebHost BuildWebHost(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("hosting.json", true)
                .AddCommandLine(args)
                .Build();

            return WebHost.CreateDefaultBuilder(args)
                .UseSetting("detailedErrors", "true")
                .UseStartup<Startup>()
                .UseUrls($"http://*:80")
                .UseConfiguration(config)
                .CaptureStartupErrors(true)
                .Build();
        }
    }
}