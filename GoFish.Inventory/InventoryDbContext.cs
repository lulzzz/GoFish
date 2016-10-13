using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace GoFish.Inventory
{
    public class InventoryDbContext : DbContext
    {
        public DbSet<StockItem> StockItems { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<StockOwner> StockOwners { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Filename=./Inventory_Database.db");
        }
    }

    public static class InventoryDbContextExtensions
    {
        public static void SeedData(this InventoryDbContext context)
        {
            EnsureDbIsCreated(context);
            AddStockOwners(context);
            AddProductTypes(context);
        }

        private static void EnsureDbIsCreated(InventoryDbContext context)
        {
            context.Database.Migrate();
        }

        private static void AddProductTypes(InventoryDbContext context)
        {
            if (context.ProductTypes.Count() == 0)
            {
                context.ProductTypes.Add(new ProductType(1, "Lobster"));
                context.ProductTypes.Add(new ProductType(2, "Cod"));
                context.ProductTypes.Add(new ProductType(3, "Halibut"));
                context.SaveChanges();
            }
        }

        private static void AddStockOwners(InventoryDbContext context)
        {
            if (context.StockOwners.Count() == 0)
            {
                context.StockOwners.Add(new StockOwner(1, "Marvin"));
                context.StockOwners.Add(new StockOwner(2, "Fred"));
                context.SaveChanges();
            }
        }
    }
}