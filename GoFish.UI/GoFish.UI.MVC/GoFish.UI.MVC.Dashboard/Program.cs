using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace GoFish.UI.MVC.Dashboard
{
    public class Program
    {
        public static void Main()
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                // .UseUrls("http://localhost:8005")
                .UseUrls("http://0.0.0.0:5005")
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<ProgramStartup>()
                .Build();

            host.Run();
        }
    }
}
