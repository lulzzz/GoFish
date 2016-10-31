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

            return View(CreateAdvertViewModel<EditAdvertViewModel>(advert));
        }

        [HttpGet]
        [Route("[action]/{advertId:Guid}")]
        public async Task<IActionResult> Summary(Guid advertId)
        {
            var jsonContent = await GetData($"adverts/{advertId}");
            return View(CreateAdvertViewModel<EditAdvertViewModel>(ParseJsonToDto(jsonContent)));
        }

        [HttpGet]
        [Route("[action]/{advertId:Guid}")]
        public async Task<IActionResult> PrePublish(Guid advertId)
        {
            var jsonContent = await GetData($"adverts/{advertId}");
            return View(CreateAdvertViewModel<PrePublishAdvertViewModel>(ParseJsonToDto(jsonContent)));
        }

        [HttpGet]
        [Route("[action]/{advertId:Guid}")]
        public async Task<IActionResult> PreDelete(Guid advertId)
        {
            var jsonContent = await GetData($"adverts/{advertId}");
            return View(CreateAdvertViewModel<EditAdvertViewModel>(ParseJsonToDto(jsonContent)));
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
                return RedirectToAction("PreDelete", "Advert", new { advertId = vm.AdvertData.Id });

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
                var response = await Delete($"adverts/{vm.AdvertData.Id}");
                // TODO: Act upon response code
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Summary", "Advert", new { advertId = vm.AdvertData.Id });
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

                var response = await PutData($"postedadverts/{advertId}", GetJsonContent(vm));
                return RedirectToAction("Published", "Advert");
                // TODO: Act upon response code
            }

            return RedirectToAction("Summary", "Advert", new { advertId = advertId });
        }

        [HttpPost]
        [Route("[action]/{advertId:Guid?}")]
        public async Task<IActionResult> Edit(Guid advertId, EditAdvertViewModel vm)
        {
            if (vm.SubmitButton == "Save")
            {
                if (!ModelState.IsValid)
                    return View(vm);

                var response = await PutData($"adverts/{vm.AdvertData.Id}", GetJsonContent(vm.AdvertData));
                // TODO: Check return codes etc. for error conditions.

                return RedirectToAction("Summary", "Advert", new { advertId = vm.AdvertData.Id });
            }

            if (vm.SubmitButton == "Cancel" && advertId != Guid.Empty)
                return RedirectToAction("Summary", "Advert", new { advertId = advertId });

            return RedirectToAction("Index", "Home");
        }

        private static StringContent GetJsonContent(object vm)
        {
            var jsonData = JsonConvert.SerializeObject(vm);
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

        private AdvertViewModel CreateAdvertViewModel<T>(AdvertDto advertData) where T : AdvertViewModel, new()
        {
            var vm = BuildBaseViewModel<T>();
            vm.AdvertData = advertData;
            return vm;
        }

        private PrePublishAdvertViewModel RehydratePrePublishViewModel(PublishAdvertViewModel vm)
        {
            var newVm = BuildBaseViewModel<PrePublishAdvertViewModel>();
            newVm.PublishType = vm.PublishType;
            newVm.AdvertData = GetAdvert(vm.Id).Result;
            return newVm;
        }

        private T BuildBaseViewModel<T>() where T : AdvertViewModel, new()
        {
            return new T()
            {
                DashboardUrl = Options.Value.DashboardUrl,
                UserName = _userDetails.GetUserName(),
                UserId = _userDetails.GetUserId()
            };
        }
    }
}