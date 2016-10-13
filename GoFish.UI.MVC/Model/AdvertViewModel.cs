using System.Collections.Generic;
using GoFish.Shared.Dto;

namespace GoFish.UI.MVC
{
    public class AdvertViewModel
    {
        public AdvertDto AdvertData { get; set; }

        public IDictionary<string, string> CatchTypes
        {
            get
            {
                return new Dictionary<string, string>
                {
                   {"1", "Lobster"},
                   {"2", "Cod"}
                };
            }
        }

        public IDictionary<string, string> Advertisers
        {
            get
            {
                return new Dictionary<string, string>
                {
                   {"1", "Beth"},
                   {"2", "Fred"}
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
                   {"2", "Net"}
                };
            }
        }

        public string SubmitButton { get; set; }
    }
}