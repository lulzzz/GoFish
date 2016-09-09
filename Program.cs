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

            AdvertiseCatch(app);
        }

        private static void AdvertiseCatch(GoFish app)
        {
            Console.WriteLine("Advertise Catch");

            var myCatch = new Catch
            (
                new CatchType(1, "Lobster")
            );

            app.Advertise(myCatch);

            Console.WriteLine("Catch Advertised");
        }
    }
}