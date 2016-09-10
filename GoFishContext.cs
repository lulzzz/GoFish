using Microsoft.EntityFrameworkCore;

namespace GoFish
{
    public class GoFishContext : DbContext
    {
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Catch> Catches { get; set; }
        public DbSet<StockItem> StockItems { get; set; }
        public DbSet<Dude> Dudes { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<Shipment> Shipments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Filename=./GoFish.db");
        }
    }
}