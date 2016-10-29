using GoFish.Shared.Dto;

namespace GoFish.UI.MVC.Advert
{
    public abstract class AdvertViewModel : UserOwnedViewModel
    {
        public AdvertDto AdvertData { get; set; }

        public string SubmitButton { get; set; }

        protected bool AdvertHasNotBeenPosted()
        {
            return AdvertData.Status == AdvertStatus.Created
                    || AdvertData.Status == AdvertStatus.Creating;
        }

        protected bool UserIsAdvertOwner()
        {
            return AdvertData.AdvertiserId == UserId;
        }
    }
}