namespace GoFish
{
    public class OrderItemIdentifier
    {
        public OrderItemIdentifier () {}
        public OrderItemIdentifier (int purchaseOrderId, int purchaseOrderItemId)
        {
            PurchaseOrderId = purchaseOrderId;
            PurchaseOrderItemId = purchaseOrderItemId;
        }

        public int Id { get; set; }
        public int PurchaseOrderId { get; set; }
        public int PurchaseOrderItemId { get; set; }
    }
}