using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GoFish.UI.MVC.Inventory
{
    [Authorize]
    public class SecureApiController : Controller
    {
        private readonly HttpClient _client;
        protected readonly IOptions<ApplicationSettings> Options;

        public SecureApiController(IOptions<ApplicationSettings> options)
        {
            _client = new HttpClient();
            Options = options;
        }

        protected async Task<string> GetData(string uri)
        {
            SetAuthToken();
            var response = await _client.GetAsync(uri);
            var content = response.Content.ReadAsStringAsync().Result;
            return content;
        }

        protected async Task<HttpResponseMessage> PutData(string uri)
        {
            SetAuthToken();
            return await _client.PutAsync(uri, new StringContent(string.Empty));
        }

        protected async Task<HttpResponseMessage> PutData(string uri, StringContent content)
        {
            SetAuthToken();
            return await _client.PutAsync(uri, content);
        }

        protected async Task<HttpResponseMessage> Delete(string uri)
        {
            SetAuthToken();
            return await _client.DeleteAsync(uri);
        }

        private void SetAuthToken()
        {
            _client.BaseAddress = new Uri(Options.Value.InventoryApiUrl);

            var accessToken = HttpContext.Authentication.GetTokenAsync("access_token").Result;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }
    }
}