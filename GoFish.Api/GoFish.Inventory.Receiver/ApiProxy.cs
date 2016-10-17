using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using GoFish.Shared.Dto;
using IdentityModel.Client;
using Newtonsoft.Json;

namespace GoFish.Inventory.Receiver
{
    public class ApiProxy
    {
        private readonly HttpClient client;

        public ApiProxy ()
        {
            client = new HttpClient();

            client.BaseAddress = new Uri("http://172.17.0.1:5002/api/"); // Inventory Api - Vagrant
            // client.BaseAddress = new Uri("http://54.171.92.206:5002/api/"); // Inventory Api - Live

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public void UpdateInventory(StockItemDto item)
        {
            SetBearerToken();
            Console.WriteLine("Bearer set");

            var jsonString = JsonConvert.SerializeObject(item);
            Console.WriteLine("Sending payload: {0}", jsonString);

            HttpResponseMessage result;
            var payload = new StringContent(jsonString, Encoding.UTF8, "application/json");
            try
            {
                result = client.PostAsync(client.BaseAddress + "inventory", payload).Result;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Exception from {0}: {1}",client.BaseAddress, ex.Message);
                throw;
            }

            Console.WriteLine("Message from {0}: {1}",client.BaseAddress, result);
        }

        private void SetBearerToken()
        {
            Console.WriteLine("Attempting connect to Identity");

            var disco = DiscoveryClient.GetAsync("http://localhost:5000").Result; // Identity Server API -- vagrant
            // var disco = DiscoveryClient.GetAsync("http://172.17.0.1:5000").Result; // Identity Server API -- live

            if(disco.IsError)
                Console.WriteLine("Error connecting to Identity");

            var tokenClient = new TokenClient(disco.TokenEndpoint, "rabbitmq", "secret");
            var tokenResponse = tokenClient.RequestClientCredentialsAsync("api1").Result;
            client.SetBearerToken(tokenResponse.AccessToken);
        }
    }
}