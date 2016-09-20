using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace GoFish.Advert.Receiver
{
    public class ApiProxy
    {
        private readonly HttpClient client;

        public ApiProxy ()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://172.17.0.1:5000/api/"); // Advert Api
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public void UpdateAdvert(int advertId)
        {
            var action = string.Format("advert/{0}/publish", advertId);
            var result = client.PutAsync(client.BaseAddress + action, new StringContent(string.Empty));
            Console.WriteLine("Message from {0}: {1}",client.BaseAddress + action, result.Result);
        }
    }
}