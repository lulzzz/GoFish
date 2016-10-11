using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;

namespace GoFish.UI.MVC
{
    public class SecureController : Controller
    {
        protected readonly HttpClient _client;
        private const string ApiBaseAddress = "http://54.171.92.206:5000/api/";

        public SecureController()
        {
            _client = new HttpClient();
        }

        protected async Task<string> GetBearerToken()
        {
            var disco = await DiscoveryClient.GetAsync("http://54.171.92.206:5002"); // Identity Server API
            var tokenClient = new TokenClient(disco.TokenEndpoint, "ro.client", "secret");
            var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync("alice", "password", "api1");
            return tokenResponse.AccessToken;
        }

        protected async Task<string> GetData(string uri)
        {
            _client.SetBearerToken(await GetBearerToken());
            var response = await _client.GetAsync($"{ApiBaseAddress}{uri}");
            var content = response.Content.ReadAsStringAsync().Result;
            return content;
        }

        protected async Task<HttpResponseMessage> PutData(string uri)
        {
            _client.SetBearerToken(await GetBearerToken());
            return await _client.PutAsync($"{ApiBaseAddress}{uri}", new StringContent(string.Empty));
        }

        protected async Task<HttpResponseMessage> Delete(string uri)
        {
            _client.SetBearerToken(await GetBearerToken());
            return await _client.DeleteAsync($"{ApiBaseAddress}{uri}");
        }

        protected async Task<HttpResponseMessage> PutData(string uri, StringContent content)
        {
            _client.SetBearerToken(await GetBearerToken());
            return await _client.PutAsync($"{ApiBaseAddress}{uri}", content);
        }
    }
}