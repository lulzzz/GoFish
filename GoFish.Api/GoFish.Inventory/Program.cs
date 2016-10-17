using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace GoFish.Inventory
{
    public class Program
    {
        public static void Main()
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                // .UseUrls("http://localhost:8002")    // Local
                .UseUrls("http://0.0.0.0:5002")         // Vagrant & Live
                .UseStartup<ProgramStartup>()
                .Build();

            host.Run();
        }
    }
}