using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GoFish.Shared.Dto;
using Newtonsoft.Json;
using System.Text;

namespace GoFish.UI.MVC
{
    [Route("[controller]")]
    public class AdvertController : SecureController
    {
        [HttpGet]
        public async Task<IActionResult> AddNew()
        {
            var client = new HttpClient();
            client.SetBearerToken(await GetBearerToken());

            var dto = new AdvertDto();
            dto.Id = System.Guid.NewGuid();
            dto.AdvertiserId = 1;
            dto.CatchTypeId = 1;
            dto.FishingMethod = 1;
            dto.Pitch = "Lobster Beauties";
            dto.Price = 100.00;
            dto.Quantity = 5;

            var s = JsonConvert.SerializeObject(dto);

            var httpContent = new StringContent(s, Encoding.UTF8, "application/json");

            // var response = await client.PostAsync("http://localhost:8081/api/adverts", httpContent);
            var response = await client.PutAsync($"http://localhost:5000/api/adverts/{dto.Id}", httpContent);
            // var response = await client.PostAsync("http://172.17.0.1:5000/api/adverts", httpContent);

            return RedirectToAction("Index", "Home");
        }
    }
}