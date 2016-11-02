using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GoFish.UI.MVC.Shared
{
    [Authorize]
    public class SecureApiController : Controller
    {
        private readonly HttpClient _client;

        public SecureApiController()
        {
            _client = new HttpClient();
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

        protected static StringContent GetJsonContent(object vm)
        {
            var jsonData = JsonConvert.SerializeObject(vm);
            var httpContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            return httpContent;
        }

        private void SetAuthToken()
        {
            var accessToken = HttpContext.Authentication.GetTokenAsync("access_token").Result;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }
    }
}