namespace GoFish.Advert
{
    public class Advert
    {
        private Advert() { }

        private Advert(int id, CatchType catchType, int quantity, double price, Advertiser advertiser)
            : this(catchType, quantity, price, advertiser)
        {
            Id = id;
        }

        private Advert(CatchType catchType, int quantity, double price, Advertiser advertiser)
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
        public string Pitch { get; internal set; }
        public FishingMethod FishingMethod { get; internal set; }
        public AdvertStatus Status { get; private set; }

        public void Post()
        {
            Status = AdvertStatus.Posted;
        }
        public void Publish()
        {
            Status = AdvertStatus.Published;
        }

        internal static Advert Add(CatchType catchType, int quantity, double price, Advertiser advertiser)
        {
            return new Advert(catchType, quantity, price, advertiser);
        }

        internal static Advert Attach(int id, CatchType catchType, int quantity, double price, Advertiser advertiser)
        {
            return new Advert(id, catchType, quantity, price, advertiser);
        }
    }
}