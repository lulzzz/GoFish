using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GoFish.UI.MVC
{
    [Authorize]
    public class SecureController : Controller
    {
        protected readonly HttpClient _client;

        private readonly IOptions<ApplicationSettings> _options;

        public SecureController(IOptions<ApplicationSettings> options)
        {
            _client = new HttpClient();
            _options = options;
        }

        protected async Task<string> GetData(string uri)
        {
            SetAuthToken();
            var response = await _client.GetAsync(_client.BaseAddress + uri);
            var content = response.Content.ReadAsStringAsync().Result;
            return content;
        }

        private void SetAuthToken()
        {
            _client.BaseAddress = new Uri(_options.Value.AdvertApiUrl);

            var accessToken = HttpContext.Authentication.GetTokenAsync("access_token").Result;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }

        protected async Task<HttpResponseMessage> PutData(string uri)
        {
            SetAuthToken();
            return await _client.PutAsync(uri, new StringContent(string.Empty));
        }

        protected async Task<HttpResponseMessage> Delete(string uri)
        {
            SetAuthToken();
            return await _client.DeleteAsync(uri);
        }

        protected async Task<HttpResponseMessage> PutData(string uri, StringContent content)
        {
            SetAuthToken();
            return await _client.PutAsync(uri, content);
        }
    }
}