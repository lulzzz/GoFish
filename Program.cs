using System;
using System.Linq;

namespace GoFish
{
    public class Program
    {
        public static void Main(string[] args)
        {
            GoFishContext ctx = new GoFishContext();
            GoFish app = new GoFish(ctx);

            AdvertiseCatch(app);
            AdvertiseCatch2(app);

            DemandAvailableStock(app);
            BuyFish(app);
            ShowPurchaseOrders(app);
        }

        private static void ShowPurchaseOrders(GoFish app)
        {
            Console.WriteLine("".PadLeft(60, '='));

            System.Console.WriteLine("Purchasing Pat\t: We now have the following purchase orders:\n");

            var orders = app.GetPurchaseOrders();
            foreach (var item in orders)
            {
                System.Console.WriteLine("\tPurchase Order {0} with {1} items",
                    item.Id,
                    item.OrderItems.ToList().Count()
                );
            }

            System.Console.WriteLine("\nPurchasing Pat\t: That's a good list of orders!");
        }

        private static void BuyFish(GoFish app)
        {
            Console.WriteLine("".PadLeft(60, '='));

            System.Console.WriteLine("Fish fan Fiona\t: I wanna buy summink!");
            System.Console.WriteLine("Fish fan Fiona\t: Ooh, you have a lobster!");

            app.Buy(new ProductType(1, "Lobster"));

            System.Console.WriteLine("Fish fan Fiona\t: Yeehaw!  Off to the nom-pot you go little fella!");

            StockList(app);
        }

        private static void DemandAvailableStock(GoFish app)
        {
            Console.WriteLine("".PadLeft(60, '='));

            System.Console.WriteLine("Fish fan Fiona\t: I wanna see the available seafood!");

            StockList(app);

            System.Console.WriteLine("Fish fan Fiona\t: Wohoo! I loves me some fish!");
        }

        private static void StockList(GoFish app)
        {
            System.Console.WriteLine("\nMerchant Marvin\t: We now have:\n");

            var stock = app.GetStock();
            foreach (var stockItem in stock)
            {
                System.Console.WriteLine("\t{0} {1} supplied by {2} at C${3}.00",
                    stockItem.Quantity,
                    Pluralize(stockItem),
                    stockItem.Seller.Name,
                    stockItem.Price.ToString().PadLeft(3, ' ')
                );
            }
            System.Console.WriteLine("\n");
        }

        private static string Pluralize(StockItem stockItem)
        {
            return stockItem.Quantity > 1 ? stockItem.Type.Name + "s": stockItem.Type.Name;
        }

        private static void AdvertiseCatch2(GoFish app)
        {
            Console.WriteLine("".PadLeft(60, '='));

            Console.WriteLine("Fisherman Henry : I'm gonna advertise my big ole Halibut haul!");

            var myCatch = new Catch
            (
                type: new ProductType(3, "Halibut"),
                quantity: 1,
                price: GeneratePrice(),
                caughtBy: new Dude(1, "Henry")
            );

            app.Advertise(myCatch);

            Console.WriteLine("Fisherman Henry : Heluva Halibut haul advertised, I 'ope sum one gonna lap it up!");

            StockList(app);
        }

        private static double GeneratePrice()
        {
            Random rnd = new Random();
            var randomPrice = rnd.Next().ToString().Substring(1,3);
            return double.Parse(randomPrice);
        }

        private static void AdvertiseCatch(GoFish app)
        {
            Console.WriteLine("".PadLeft(60, '='));

            Console.WriteLine("Fisherman Henry : I'm gonna advertise my big ole lobster catch!");

            var myCatch = new Catch
            (
                type: new ProductType(1, "Lobster"),
                quantity: 1,
                price: GeneratePrice(),
                caughtBy: new Dude(1, "Henry")
            );

            app.Advertise(myCatch);

            Console.WriteLine("Fisherman Henry : Catch advertised, I 'ope sum one gonna buy it!");

            StockList(app);
        }
    }
}