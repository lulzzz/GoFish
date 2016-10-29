using System.Collections.Generic;

namespace GoFish.UI.MVC.Advert
{
    public class EditAdvertViewModel : AdvertViewModel
    {
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
                   {"1", "Line"},
                   {"2", "Net"},
                   {"3", "Basket"}
                };
            }
        }

        public string EditButtonState
        {
            get
            {
                return AdvertHasNotBeenPosted()
                    && UserIsAdvertOwner() ? string.Empty : "disabled";
            }
        }

        public string PublishButtonState
        {
            get
            {
                return AdvertHasNotBeenPosted()
                    && UserIsAdvertOwner() ? string.Empty : "disabled";
            }
        }

        public string DeleteButtonState
        {
            get
            {
                return UserIsAdvertOwner() ? string.Empty : "disabled";
            }
        }

        public string RemovalText
        {
            get
            {
                return AdvertHasNotBeenPosted() ? "Delete" : "Withdraw";
            }
        }

        public string GetToolTip(string button)
        {
            if (button == "Delete")
            {
                return UserIsAdvertOwner() ? string.Empty : "You can not do this to someone else's advert";
            }
            else
            {
                if (!UserIsAdvertOwner())
                    return "You can not do this to someone else's advert";

                return AdvertHasNotBeenPosted() ? string.Empty : $"You can not do this to a {AdvertData.Status} advert.";
            }
        }
    }
}