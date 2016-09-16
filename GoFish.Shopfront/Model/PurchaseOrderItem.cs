namespace GoFish
{
    public class PurchaseOrderItem
    {
        public PurchaseOrderItem () {}
        public PurchaseOrderItem (int sku)
        {
            StockItem = sku;
        }

        public int Id { get; private set; }

        public int StockItem { get; private set; }
    }
}