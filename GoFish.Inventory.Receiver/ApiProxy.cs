using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using GoFish.Shared.Dto;
using Newtonsoft.Json;

namespace GoFish.Inventory.Receiver
{
    public class ApiProxy
    {
        private readonly HttpClient client;

        public ApiProxy ()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://172.17.0.1:5001/api/"); // Inventory Api
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public void UpdateInventory(StockItemDto item)
        {
            var jsonString = JsonConvert.SerializeObject(item);
            System.Console.WriteLine("Sending payload: {0}", jsonString);

            var payload = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var result = client.PostAsync(client.BaseAddress + "inventory", payload);

            Console.WriteLine("Message from {0}: {1}",client.BaseAddress, result.Result);
        }
    }
}