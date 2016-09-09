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

            AddCatchTypes(app);

            AdvertiseCatch(app);
        }

        private static void AddCatchTypes(GoFish app)
        {
            app.AddCatchTypes();
        }

        private static void AdvertiseCatch(GoFish app)
        {
            Console.WriteLine("I'm Gonna Advertise my Catch!");

            var myCatch = new Catch
            (
                new CatchType(1, "Lobster")
            );

            app.Advertise(myCatch);

            Console.WriteLine("Catch Advertised, I 'ope sum one gonna buy it!");
        }
    }
}