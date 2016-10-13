using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace GoFish.Advert
{
    public class AdvertisingDbContext : DbContext
    {
        public DbSet<Advert> Adverts { get; set; }
        public DbSet<Advertiser> Advertisers { get; set; }
        public DbSet<CatchType> CatchTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Filename=./Advert_Database.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Advert>()
                .Ignore(b => b.History);
        }
    }

    internal static class AdvertisingDbContextExtensions
    {
        public static void SeedData(this AdvertisingDbContext context)
        {
            EnsureDBIsCreated(context);
            AddCatchTypes(context);
            AddAdvertisers(context);
        }

        private static void EnsureDBIsCreated(AdvertisingDbContext context)
        {
            context.Database.Migrate();
        }

        private static void AddAdvertisers(AdvertisingDbContext context)
        {
            if (context.Advertisers.Count() == 0)
            {
                context.Advertisers.Add(new Advertiser(1, "Beth"));
                context.Advertisers.Add(new Advertiser(2, "Fred"));
                context.SaveChanges();
            }
        }

        private static void AddCatchTypes(AdvertisingDbContext context)
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