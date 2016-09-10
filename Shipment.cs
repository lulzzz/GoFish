using System;
using System.Collections.Generic;

namespace GoFish
{
    public class Shipment
    {
        private Shipment () {}

        public Shipment (string shipFrom, string shipTo)
        {
            From = shipFrom;
            To = shipTo;
            PurchaseOrderItems = new List<OrderItemIdentifier>();
        }

        public int Id { get; private set; }

        public string From { get; private set; }

        public string To { get; private set; }

        public IList<OrderItemIdentifier> PurchaseOrderItems { get; set; }

        public DateTime Date { get; set; }
    }
}