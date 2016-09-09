using Microsoft.EntityFrameworkCore;

namespace GoFish
{
    public class GoFishContext : DbContext
    {
        public DbSet<CatchType> CatchTypes { get; set; }
        public DbSet<Catch> Catches { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Filename=./GoFish.db");
        }
    }
}