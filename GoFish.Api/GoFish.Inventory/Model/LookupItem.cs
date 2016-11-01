using System;
using System.Linq;

namespace GoFish.Inventory
{
    public abstract class LookupItem
    {
        public int Id { get; internal set; }
        public string Name { get; internal set; }

        public static T GetFromCache<T>(int lookupId) where T : LookupItem
        {
            LookupItem item;
            Type lookupType = typeof(T);

            // Needs to come from REDIS but this'll do for now
            var repo = new InventoryDbContext();
            if (lookupType == typeof(ProductType))
            {
                item = repo.ProductTypes.Where(i => i.Id == lookupId).Single();
            }
            else
            {
                item = repo.StockOwners.Where(i => i.Id == lookupId).Single();
            }

            return (T)Activator.CreateInstance(typeof(T), new object[] { item.Id, item.Name });
        }
    }
}