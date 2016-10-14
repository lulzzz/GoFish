using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace GoFish.UI.MVC
{
    public class Program
    {
        public static void Main()
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseUrls("http://0.0.0.0:5003") // Vagrant
                // .UseUrls("http://localhost:8003") // Local
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<ProgramStartup>()
                .Build();

            host.Run();
        }
    }
}