using System;
using System.Net.Http;
using System.Net.Http.Headers;
using IdentityModel.Client;

namespace GoFish.Advert.Receiver
{
    public class ApiProxy
    {
        private readonly HttpClient client;

        public ApiProxy()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://172.17.0.1:5000/api/"); // Advert Api
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public void UpdateAdvert(int advertId)
        {
            SetBearerToken();

            var action = string.Format("publishedadverts/{0}", advertId);
            var result = client.PutAsync(client.BaseAddress + action, new StringContent(string.Empty));
            Console.WriteLine("Message from {0}: {1}", client.BaseAddress + action, result.Result);
        }

        private void SetBearerToken()
        {
            var disco = DiscoveryClient.GetAsync("http://172.17.0.1:5002").Result; // Identity Server API
            var tokenClient = new TokenClient(disco.TokenEndpoint, "rabbitmq", "secret");
            var tokenResponse = tokenClient.RequestClientCredentialsAsync("api1").Result;
            client.SetBearerToken(tokenResponse.AccessToken);
        }
    }
}