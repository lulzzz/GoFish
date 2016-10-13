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
                .UseUrls("http://localhost:5003") // Local
                // .UseUrls("http://0.0.0.0:5003") // Vagrant
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<ProgramStartup>()
                .Build();

            host.Run();
        }
    }
}