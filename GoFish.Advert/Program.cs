using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace GoFish.Advert
{
    public class Program
    {
        public static void Main()
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseUrls("http://0.0.0.0:5001")      // Vagrant
                // .UseUrls("http://localhost:8001")    // Local
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<ProgramStartup>()
                .Build();

            host.Run();
        }
    }
}