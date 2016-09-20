namespace GoFish.Advert
{
    public class Advert
    {
        private Advert() { }

        public Advert(CatchType catchType, int quantity, double price, Advertiser advertiser)
        {
            CatchType = catchType;
            Quantity = quantity;
            Price = price;
            Advertiser = advertiser;
            Status = AdvertStatus.Created;
        }

        public int Id { get; private set; }
        public CatchType CatchType { get; private set; }
        public int Quantity { get; private set; }
        public double Price { get; private set; }
        public Advertiser Advertiser { get; private set; }
        public AdvertStatus Status { get; private set; }

        public void Post()
        {
            Status = AdvertStatus.Posted;
        }
        public void Publish()
        {
             Status = AdvertStatus.Published;
        }
    }
}