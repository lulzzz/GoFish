using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace GoFish.Identity
{
    public class Program
    {
        public static void Main()
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                // .UseUrls("http://localhost:8000/")   // Local
                .UseUrls("http://0.0.0.0:5000/")     // Vagrant & Live
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<ProgramStartup>()
                .Build();

            host.Run();
        }
    }
}
