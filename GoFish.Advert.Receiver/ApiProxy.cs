using System;
using System.Net.Http;
using System.Net.Http.Headers;
using IdentityModel.Client;
using Newtonsoft.Json;

namespace GoFish.Advert.Receiver
{
    public class ApiProxy
    {
        private readonly HttpClient client;

        public ApiProxy()
        {
            client = new HttpClient();

            // client.BaseAddress = new Uri("http://172.17.0.1:5001/api/");    // Advert Api -- Vagrant
            client.BaseAddress = new Uri("http://54.171.92.206:5001/api/"); // Advert Api -- Live

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public void UpdateAdvert(Guid advertId)
        {
            SetBearerToken();
            Console.WriteLine("Bearer set");

            Console.WriteLine($"About to publish {advertId}");

            var jsonString = JsonConvert.SerializeObject(advertId);

            HttpResponseMessage result;
            try
            {
                result = client.PutAsync($"{client.BaseAddress}publishedadverts/{advertId}", new StringContent(string.Empty)).Result;
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
            Console.WriteLine("Attempt to get auth token");

            // var disco = DiscoveryClient.GetAsync("http://localhost:5000").Result; // Identity Server API -- Vagrant
            var disco = DiscoveryClient.GetAsync("http://172.17.0.1:5000").Result; // Identity Server API -- Live

            var tokenClient = new TokenClient(disco.TokenEndpoint, "rabbitmq", "secret");
            var tokenResponse = tokenClient.RequestClientCredentialsAsync("api1").Result;
            client.SetBearerToken(tokenResponse.AccessToken);
        }
    }
}