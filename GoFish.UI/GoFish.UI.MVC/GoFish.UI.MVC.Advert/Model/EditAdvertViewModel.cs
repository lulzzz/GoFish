namespace GoFish.UI.MVC.Advert
{
    public class EditAdvertViewModel : AdvertViewModel
    {
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
                if (AdvertIsNew())
                    return "disabled";

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

            return AdvertHasNotBeenPosted() ? string.Empty : $"You can not do this.  The advert status is {AdvertData.Status}.";
        }
    }
}