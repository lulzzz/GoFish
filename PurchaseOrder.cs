using System.Collections.Generic;

namespace GoFish
{
    public class PurchaseOrder
    {
        private IList<PurchaseOrderItem> _orderItems;

        public PurchaseOrder ()
        {
            _orderItems = new List<PurchaseOrderItem>();
        }

        public int Id { get; private set; }

        public void AddOrderItem(PurchaseOrderItem item)
        {
            _orderItems.Add(item);
        }

        public IList<PurchaseOrderItem> OrderItems
        {
            get { return _orderItems;}
        }
    }
}