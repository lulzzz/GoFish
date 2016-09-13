using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace GoFish.Advert
{
    public class AdvertisingContext : DbContext
    {
        public DbSet<Advert> Adverts { get; set; }
        public DbSet<Advertiser> Advertisers { get; set; }
        public DbSet<CatchType> CatchTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Filename=./Advert_Database.db");
        }
    }

    public static class AdvertisingContextExtensions
    {
        public static void SeedData(this AdvertisingContext context)
        {
            AddCatchTypes(context);
            AddAdvertisers(context);
        }

        private static void AddAdvertisers(AdvertisingContext context)
        {
            if (context.Advertisers.Count() == 0)
            {
                context.Advertisers.Add(new Advertiser(1, "Henry"));
                context.Advertisers.Add(new Advertiser(2, "Marvin"));
                context.SaveChanges();
            }
        }

        private static void AddCatchTypes(AdvertisingContext context)
        {
            if (context.CatchTypes.Count() == 0)
            {
                context.CatchTypes.Add(new CatchType(1, "Lobster"));
                context.CatchTypes.Add(new CatchType(2, "Cod"));
                context.CatchTypes.Add(new CatchType(3, "Halibut"));
                context.SaveChanges();
            }
        }
    }
}