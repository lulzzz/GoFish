using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using GoFish.Shared.Dto;
using GoFish.UI.MVC.Shared;

namespace GoFish.UI.MVC.Advert
{
    [Route("[controller]")]
    public class AdvertController : SecureApiController
    {
        private readonly IUserDetails _userDetails;
        private readonly IOptions<ApplicationSettings> _options;

        public AdvertController(IOptions<ApplicationSettings> options, IUserDetails userDetails)
        {
            _userDetails = userDetails;
            _options = options;
        }

        [HttpGet]
        [Route("[action]/{advertId:Guid?}")]
        public async Task<IActionResult> Edit(Guid? advertId)
        {
            var advert = new AdvertDto();
            if (advertId.HasValue)
                advert = await GetAdvert((Guid)advertId);

            return View(CreateAdvertViewModel<EditAdvertViewModel>(advert));
        }

        [HttpGet]
        [Route("[action]/{advertId:Guid}")]
        public async Task<IActionResult> Summary(Guid advertId)
        {
            var jsonContent = await GetData($"{_options.Value.AdvertApiUrl}adverts/{advertId}");
            return View(CreateAdvertViewModel<EditAdvertViewModel>(ParseJsonToDto(jsonContent)));
        }

        [HttpGet]
        [Route("[action]/{advertId:Guid}")]
        public async Task<IActionResult> PrePublish(Guid advertId)
        {
            var jsonContent = await GetData($"{_options.Value.AdvertApiUrl}adverts/{advertId}");
            return View(CreateAdvertViewModel<PrePublishAdvertViewModel>(ParseJsonToDto(jsonContent)));
        }

        [HttpGet]
        [Route("[action]/{advertId:Guid}")]
        public async Task<IActionResult> PreDelete(Guid advertId, string referringPage)
        {
            var jsonContent = await GetData($"{_options.Value.AdvertApiUrl}adverts/{advertId}");
            var vm = CreateAdvertViewModel<EditAdvertViewModel>(ParseJsonToDto(jsonContent));
            vm.ReferringPage = referringPage;
            return View(vm);
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult Published()
        {
            return View(new UserOwnedViewModel() { UserName = _userDetails.GetUserName() });
        }

        [HttpPost]
        [Route("[action]/{advertId:Guid}")]
        public IActionResult Summary(Guid advertId, EditAdvertViewModel vm)
        {
            if (vm.SubmitButton == "Edit")
                return RedirectToAction("Edit", "Advert", new { advertId = vm.AdvertData.Id });

            if (vm.SubmitButton == "Delete")
                return RedirectToAction("PreDelete", "Advert", new { advertId = vm.AdvertData.Id, referringPage = "Summary" });

            if (vm.SubmitButton == "Publish")
                return RedirectToAction("PrePublish", "Advert");

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Delete(EditAdvertViewModel vm)
        {
            if (vm.SubmitButton == "Delete")
            {
                var response = await Delete($"{_options.Value.AdvertApiUrl}adverts/{vm.AdvertData.Id}"); // TODO: Act upon response code
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction(WhiteListReferrers(vm.ReferringPage), "Advert", new { advertId = vm.AdvertData.Id });
        }

        private string WhiteListReferrers(string referringPage)
        {
            return (referringPage != "Summary" && referringPage != "Edit") ? "Summary" : referringPage;
        }

        [HttpPost]
        [Route("[action]/{advertId:Guid}")]
        public async Task<IActionResult> Publish(Guid advertId, PublishAdvertViewModel vm)
        {
            if (vm.SubmitButton == "Publish")
            {
                vm.Id = advertId;

                if (!ModelState.IsValid)
                    return View("PrePublish", RehydratePrePublishViewModel(vm));

                var response = await PutData($"{_options.Value.AdvertApiUrl}postedadverts/{advertId}", GetJsonContent(vm)); // TODO: Act upon response code
                return RedirectToAction("Published", "Advert");
            }

            return RedirectToAction("Summary", "Advert", new { advertId = advertId });
        }

        [HttpPost]
        [Route("[action]/{advertId:Guid?}")]
        public async Task<IActionResult> Edit(Guid advertId, EditAdvertViewModel vm)
        {
            if (vm.SubmitButton == "Save")
            {
                if (!ModelState.IsValid) return View(vm);

                var response = await PutData($"{_options.Value.AdvertApiUrl}adverts/{vm.AdvertData.Id}", GetJsonContent(vm.AdvertData)); // TODO: Check return codes etc. for error conditions.
                return RedirectToAction("Summary", "Advert", new { advertId = vm.AdvertData.Id });
            }

            if (vm.SubmitButton == "Delete")
                return RedirectToAction("PreDelete", "Advert", new { advertId = vm.AdvertData.Id, referringPage = "Edit" });

            if (vm.SubmitButton == "Cancel" && advertId != Guid.Empty)
                return RedirectToAction("Summary", "Advert", new { advertId = advertId });

            return RedirectToAction("Index", "Home");
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
            var jsonContent = await GetData($"{_options.Value.AdvertApiUrl}adverts/{advertId}");
            return ParseJsonToDto(jsonContent);
        }

        private AdvertViewModel CreateAdvertViewModel<T>(AdvertDto advertData) where T : AdvertViewModel, new()
        {
            advertData.AdvertiserId = _userDetails.GetUserId();
            return BuildBaseViewModel<T>(advertData);
        }

        private PrePublishAdvertViewModel RehydratePrePublishViewModel(PublishAdvertViewModel vm)
        {
            var advertData = GetAdvert(vm.Id).Result;
            var newVm = BuildBaseViewModel<PrePublishAdvertViewModel>(advertData);
            newVm.PublishType = vm.PublishType;
            return newVm;
        }

        private T BuildBaseViewModel<T>(AdvertDto advertData) where T : AdvertViewModel, new()
        {
            return new T()
            {
                DashboardUrl = _options.Value.DashboardUrl,
                UserName = _userDetails.GetUserName(),
                UserId = _userDetails.GetUserId(),
                AdvertData = advertData
            };
        }
    }
}