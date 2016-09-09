namespace GoFish
{
    public class GoFish
    {
        private readonly GoFishContext _context;

        public GoFish (GoFishContext context)
        {
            _context = context;
        }

        public void Advertise(Catch advert) {
            // _context.CatchTypes.Add(advert.Type);
            _context.Catches.Add(advert);
            _context.SaveChanges();
        }
    }
}