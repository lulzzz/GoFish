using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System;
using GoFish.Shared.Dto;
using Newtonsoft.Json.Linq;

namespace GoFish.UI.MVC
{
    [Route("[controller]")]
    public class AdvertController : SecureController
    {
        private readonly HttpClient _client;
        public AdvertController()
        {
            _client = new HttpClient();
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Edit(Guid? advertId)
        {
            AdvertDto advert;
            if (advertId.HasValue)
            {
                advert = await GetAdvert((Guid)advertId);
            }
            else
            {
                advert = new AdvertDto();
            }

            var vm = new AdvertViewModel { AdvertData = advert };
            return View(vm);
        }

        private async Task<AdvertDto> GetAdvert(Guid advertId)
        {
            _client.SetBearerToken(await GetBearerToken());

            var response = await _client.GetAsync($"http://54.171.92.206:5000/api/adverts/{advertId}");
            var content = response.Content.ReadAsStringAsync().Result;

            return ParseJsonToDto(content);
        }

        private static AdvertDto ParseJsonToDto(string content)
        {
            // TODO: Consider a custom JSON serialiser
            var jsonObject = JObject.Parse(content);
            AdvertDto advert = jsonObject.ToObject<AdvertDto>();
            advert.AdvertiserId = (int)jsonObject.SelectToken("advertiser.id");
            advert.CatchTypeId = (int)jsonObject.SelectToken("catchType.id");
            return advert;
        }

        [HttpGet]
        [Route("[action]/{advertId:Guid}")]
        public async Task<IActionResult> PrePublish(Guid advertId)
        {
            _client.SetBearerToken(await GetBearerToken());

            var response = await _client.GetAsync($"http://54.171.92.206:5000/api/adverts/{advertId}");
            var content = response.Content.ReadAsStringAsync().Result;
            var vm = new AdvertViewModel()
            {
                // CatchType = (string)jsonObject.SelectToken("catchtype.name"),
                AdvertData = ParseJsonToDto(content)
            };

            return View(vm);
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Publish(AdvertViewModel vm)
        {
            if (vm.SubmitButton == "Edit")
                return RedirectToAction("Edit", "Advert", new { advertId = vm.AdvertData.Id });

            if (vm.SubmitButton == "Publish")
            {
                // TODO: Publish the Advert
                return RedirectToAction("Published", "Advert");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult Published()
        {
            return View();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Edit(AdvertViewModel vm)
        {
            if (vm.SubmitButton == "Save")
            {
                if (!ModelState.IsValid)
                    return View(vm);

                _client.SetBearerToken(await GetBearerToken());

                var response = await
                    _client.PutAsync($"http://54.171.92.206:5000/api/adverts/{vm.AdvertData.Id}",
                    GetContentFromModel(vm));

                // TODO: Act on Response code, return any errors to client

                return RedirectToAction("PrePublish", "Advert", new { advertId = vm.AdvertData.Id });
            }

            return RedirectToAction("Index", "Home");
        }

        private static StringContent GetContentFromModel(AdvertViewModel vm)
        {
            var jsonData = JsonConvert.SerializeObject(vm.AdvertData);
            var httpContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            return httpContent;
        }
    }
}