using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace GoFish.Identity
{
    public class Program
    {
        public static void Main()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("hosting.json", optional: true)
                .Build();

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseStartup<ProgramStartup>()
                .UseConfiguration(config)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .Build();

            host.Run();
        }
    }
}
