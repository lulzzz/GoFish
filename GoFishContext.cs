using Microsoft.EntityFrameworkCore;

namespace GoFish
{
    public class GoFishContext : DbContext
    {
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Catch> Catches { get; set; }
        public DbSet<StockItem> StockItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Filename=./GoFish.db");
        }
    }
}