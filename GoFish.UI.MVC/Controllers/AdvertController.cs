using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System;
using GoFish.Shared.Dto;

namespace GoFish.UI.MVC
{
    [Route("[controller]")]
    public class AdvertController : SecureController
    {
        [HttpGet]
        public IActionResult Add()
        {
            var advert = new AdvertDto()
            {
                Id = Guid.NewGuid()
            };

            var vm = new AdvertViewModel { AdvertData = advert };
            return View(vm);
        }

        [HttpGet("{advertId}")]
        public IActionResult Edit(Guid advertId)
        {
            var advert = new AdvertDto()
            {
                Id = advertId
            };

            return View(new AdvertViewModel { AdvertData = advert });
        }

        [HttpPost]
        public async Task<IActionResult> Add(AdvertViewModel vm)
        {
            if(!ModelState.IsValid)
                return View(vm);

            var client = new HttpClient();
            client.SetBearerToken(await GetBearerToken());

            var jsonData = JsonConvert.SerializeObject(vm.AdvertData);
            var httpContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"http://54.171.92.206:5000/api/adverts/{vm.AdvertData.Id}", httpContent);

            // TODO: Act on Response code, return any errors to client

            return RedirectToAction("Index", "Home");
        }
    }
}