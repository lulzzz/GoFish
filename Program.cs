using System;

namespace GoFish
{
    public class Program
    {
        public static void Main(string[] args)
        {
            GoFishContext ctx = new GoFishContext();
            GoFish app = new GoFish(ctx);

            Console.WriteLine("Go Fish!");
            Console.WriteLine("".PadLeft(60, '='));

            AdvertiseCatch(app);

            SeeAvailableCatches(app);
        }

        private static void SeeAvailableCatches(GoFish app)
        {
            System.Console.WriteLine("Fishfan Fiona\t: I wanna see the available seafood!\n");

            System.Console.WriteLine("Merchant Marvin\t: We have:\n");
            var stock = app.GetStock();
            foreach (var stockItem in stock)
            {
                System.Console.WriteLine("\t{0}", stockItem.Name);
            }

            System.Console.WriteLine("\nFishfan Fiona\t: Wohoo! I loves me some fish!");

            Console.WriteLine("".PadLeft(60, '='));
        }

        private static void AdvertiseCatch(GoFish app)
        {
            Console.WriteLine("Fisherman Henry : I'm gonna advertise my big ole lobster catch!");

            var myCatch = new Catch
            (
                new CatchType(1, "Lobster")
            );

            app.Advertise(myCatch);

            Console.WriteLine("Fisherman Henry : Catch advertised, I 'ope sum one gonna buy it!");
            Console.WriteLine("".PadLeft(60, '='));
        }
    }
}