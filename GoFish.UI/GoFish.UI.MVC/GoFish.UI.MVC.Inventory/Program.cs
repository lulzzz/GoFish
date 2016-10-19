using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace GoFish.UI.MVC.Inventory
{
    public class Program
    {
        public static void Main()
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<ProgramStartup>()
                .Build();

            host.Run();
        }
    }
}
