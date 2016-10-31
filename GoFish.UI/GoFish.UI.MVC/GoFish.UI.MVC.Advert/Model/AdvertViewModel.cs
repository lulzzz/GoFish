using System.Collections.Generic;
using GoFish.Shared.Dto;

namespace GoFish.UI.MVC.Advert
{
    public abstract class AdvertViewModel : UserOwnedViewModel
    {
        public AdvertDto AdvertData { get; set; }

        public IDictionary<string, string> CatchTypes
        {
            get
            {
                return new Dictionary<string, string>
                {
                   {"1", "Lobster"},
                   {"2", "Cod"},
                   {"3", "Halibut"}
                };
            }
        }

        public IDictionary<string, string> FishingMethods
        {
            get
            {
                return new Dictionary<string, string>
                {
                   {"0", "-- Detail Unknown --"},
                   {"1", "Line"},
                   {"2", "Net"},
                   {"3", "Basket"}
                };
            }
        }

        public string SubmitButton { get; set; }

        protected bool AdvertHasNotBeenPosted()
        {
            return AdvertData.Status == AdvertStatus.Created
                    || AdvertData.Status == AdvertStatus.Creating;
        }

        protected bool AdvertIsNew()
        {
            return AdvertData.Status == AdvertStatus.Creating;
        }

        protected bool UserIsAdvertOwner()
        {
            return AdvertData.AdvertiserId == UserId;
        }

        private string _referringPage;
        public string ReferringPage
        {
            get
            {
                return _referringPage == string.Empty || _referringPage == null
                    ? "Summary" : _referringPage;
            }
            set
            {
                _referringPage = value;
            }
        }
    }
}