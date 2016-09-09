using System;

namespace GoFish
{
    public class Program
    {
        public static void Main(string[] args)
        {
            GoFishContext ctx = new GoFishContext();
            GoFish app = new GoFish(ctx);

            Console.WriteLine("".PadLeft(60, '='));
            Console.WriteLine("Go Fish!   Version: 0.0.1");
            Console.WriteLine("".PadLeft(60, '='));

            AdvertiseCatch(app);

            SeeAvailableStock(app);

            AdvertiseCatch2(app);

            SeeAvailableStock(app);

            BuyFish(app);
        }

        private static void BuyFish(GoFish app)
        {
            System.Console.WriteLine("\nFishfan Fiona\t: I wanna buy summink!");
            System.Console.WriteLine("Fishfan Fiona\t: Ooh, you have a lobster!");

            app.Buy(new CatchType(1, "Lobster"));

            System.Console.WriteLine("Fishfan Fiona\t: Yeehaw!  Off to the nom-pot you go little fella!");
            Console.WriteLine("".PadLeft(60, '='));

            StockList(app);
        }

        private static void SeeAvailableStock(GoFish app)
        {
            System.Console.WriteLine("\nFishfan Fiona\t: I wanna see the available seafood!\n");

            StockList(app);

            System.Console.WriteLine("\nFishfan Fiona\t: Wohoo! I loves me some fish!");

            Console.WriteLine("".PadLeft(60, '='));
        }

        private static void StockList(GoFish app)
        {
            System.Console.WriteLine("Merchant Marvin\t: We have:\n");
            var stock = app.GetStock();
            foreach (var stockItem in stock)
            {
                System.Console.WriteLine("\t{0} {1}", stockItem.Quantity, Pluralize(stockItem));
            }
        }

        private static string Pluralize(StockItem stockItem)
        {
            return stockItem.Quantity > 1 ? stockItem.Name + "s": stockItem.Name;
        }

        private static void AdvertiseCatch(GoFish app)
        {
            Console.WriteLine("\nFisherman Henry : I'm gonna advertise my big ole lobster catch!");

            var myCatch = new Catch
            (
                type: new CatchType(1, "Lobster"),
                quantity: 1
            );

            app.Advertise(myCatch);

            Console.WriteLine("Fisherman Henry : Catch advertised, I 'ope sum one gonna buy it!");
            Console.WriteLine("".PadLeft(60, '='));
        }

        private static void AdvertiseCatch2(GoFish app)
        {
            Console.WriteLine("\nFisherman Henry : I'm gonna advertise my big ole Halibut haul!");

            var myCatch = new Catch
            (
                type: new CatchType(3, "Halibut"),
                quantity: 1
            );

            app.Advertise(myCatch);

            Console.WriteLine("Fisherman Henry : Heluva Halibut haul advertised, I 'ope sum one gonna lap it up!");
            Console.WriteLine("".PadLeft(60, '='));
        }
    }
}