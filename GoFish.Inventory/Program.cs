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
                .UseUrls("http://0.0.0.0:5002")
                // .UseUrls("http://localhost:8002")
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}