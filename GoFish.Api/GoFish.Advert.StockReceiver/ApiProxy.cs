using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using GoFish.Shared.Dto;
using IdentityModel.Client;
using Newtonsoft.Json;

namespace GoFish.Advert.StockReceiver
{
    public class ApiProxy
    {
        private readonly HttpClient client;

        public ApiProxy()
        {
            client = new HttpClient();

            // client.BaseAddress = new Uri("http://localhost:8001/api/");    // Advert Api -- Local
            client.BaseAddress = new Uri("http://172.17.0.1:5001/api/");    // Advert Api -- Vagrant
            // client.BaseAddress = new Uri("http://54.171.92.206:5001/api/"); // Advert Api -- Live

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public void UpdateAdvert(StockItemDto stockItem)
        {
            SetBearerToken();

            var jsonString = JsonConvert.SerializeObject(stockItem);
            var payload = new StringContent(jsonString, Encoding.UTF8, "application/json");

            HttpResponseMessage result;
            try
            {
                result = client.PutAsync($"{client.BaseAddress}publishedadverts/stockchange/{stockItem.AdvertId}", payload).Result;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Exception from {0}: {1}",client.BaseAddress, ex.Message);
                throw;
            }
        }

        private void SetBearerToken()
        {
            var disco = DiscoveryClient.GetAsync("http://localhost:5000").Result; // Identity Server API -- Vagrant
            // var disco = DiscoveryClient.GetAsync("http://172.17.0.1:5000").Result; // Identity Server API -- Live

            var tokenClient = new TokenClient(disco.TokenEndpoint, "rabbitmq", "secret");
            var tokenResponse = tokenClient.RequestClientCredentialsAsync("gofish.messaging").Result;
            client.SetBearerToken(tokenResponse.AccessToken);
        }
    }
}