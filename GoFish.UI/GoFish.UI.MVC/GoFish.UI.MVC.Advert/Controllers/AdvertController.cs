using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System;
using GoFish.Shared.Dto;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Options;
using GoFish.UI.MVC.Shared;

namespace GoFish.UI.MVC.Advert
{
    [Route("[controller]")]
    public class AdvertController : SecureApiController
    {
        private readonly IUserDetails _userDetails;

        public AdvertController(IOptions<ApplicationSettings> options, IUserDetails userDetails) : base(options)
        {
            _userDetails = userDetails;
        }

        [HttpGet]
        [Route("[action]/{advertId:Guid?}")]
        public async Task<IActionResult> Edit(Guid? advertId)
        {
            var advert = new AdvertDto();
            if (advertId.HasValue)
                advert = await GetAdvert((Guid)advertId);

            return View(CreateAdvertViewModel(advert));
        }

        [HttpGet]
        [Route("[action]/{advertId:Guid}")]
        public async Task<IActionResult> Summary(Guid advertId)
        {
            var jsonContent = await GetData($"adverts/{advertId}");
            return View(CreateAdvertViewModel(ParseJsonToDto(jsonContent)));
        }

        [HttpGet]
        [Route("[action]/{advertId:Guid}")]
        public async Task<IActionResult> PrePublish(Guid advertId)
        {
            var jsonContent = await GetData($"adverts/{advertId}");
            return View(CreateAdvertViewModel(ParseJsonToDto(jsonContent)));
        }

        [HttpGet]
        [Route("[action]/{advertId:Guid}")]
        public async Task<IActionResult> PreDelete(Guid advertId)
        {
            var jsonContent = await GetData($"adverts/{advertId}");
            return View(CreateAdvertViewModel(ParseJsonToDto(jsonContent)));
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult Published()
        {
            return View(new UserOwnedViewModel() { UserName = _userDetails.GetUserName() });
        }

        [HttpPost]
        [Route("[action]/{advertId:Guid}")]
        public IActionResult Summary(Guid advertId, AdvertViewModel vm)
        {
            if (vm.SubmitButton == "Edit")
                return RedirectToAction("Edit", "Advert", new { advertId = vm.AdvertData.Id });

            if (vm.SubmitButton == "Delete")
                return RedirectToAction("PreDelete", "Advert", new { advertId = vm.AdvertData.Id });

            if (vm.SubmitButton == "Publish")
                return RedirectToAction("PrePublish", "Advert");

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Delete(AdvertViewModel vm)
        {
            if (vm.SubmitButton == "Delete")
            {
                var response = await Delete($"adverts/{vm.AdvertData.Id}");
                // TODO: Act upon response code
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [Route("[action]/{advertId:Guid}")]
        public async Task<IActionResult> Publish(Guid advertId, AdvertViewModel vm)
        {
            if (vm.SubmitButton == "Publish")
            {
                var response = await PutData($"postedadverts/{advertId}");
                return RedirectToAction("Published", "Advert");
                // TODO: Act upon response code
            }

            return RedirectToAction("Summary", "Advert", new { advertId = advertId });
        }

        [HttpPost]
        [Route("[action]/{advertId:Guid?}")]
        public async Task<IActionResult> Edit(Guid advertId, AdvertViewModel vm)
        {
            if (vm.SubmitButton == "Save")
            {
                if (!ModelState.IsValid)
                    return View(vm);

                var response = await PutData($"adverts/{vm.AdvertData.Id}", GetJsonContentFromModel(vm));
                // TODO: Check return codes etc. for error conditions.

                return RedirectToAction("Summary", "Advert", new { advertId = vm.AdvertData.Id });
            }

            if (vm.SubmitButton == "Cancel" && advertId != Guid.Empty)
                return RedirectToAction("Summary", "Advert", new { advertId = advertId });

            return RedirectToAction("Index", "Home");
        }

        private static StringContent GetJsonContentFromModel(AdvertViewModel vm)
        {
            var jsonData = JsonConvert.SerializeObject(vm.AdvertData);
            var httpContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            return httpContent;
        }

        private static AdvertDto ParseJsonToDto(string content)
        {
            var jsonObject = JObject.Parse(content);
            AdvertDto advert = jsonObject.ToObject<AdvertDto>();
            advert.AdvertiserId = (int)jsonObject.SelectToken("advertiser.id");
            advert.CatchTypeId = (int)jsonObject.SelectToken("catchType.id");
            return advert;
        }

        private async Task<AdvertDto> GetAdvert(Guid advertId)
        {
            var jsonContent = await GetData($"adverts/{advertId}");
            return ParseJsonToDto(jsonContent);
        }

        private AdvertViewModel CreateAdvertViewModel(AdvertDto advertData)
        {
            return new AdvertViewModel()
            {
                DashboardUrl = Options.Value.DashboardUrl,
                UserName = _userDetails.GetUserName(),
                UserId = _userDetails.GetUserId(),
                AdvertData = advertData
            };
        }
    }
}