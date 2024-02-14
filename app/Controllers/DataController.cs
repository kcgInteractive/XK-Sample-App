using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;

namespace YourNamespace.Controllers
{
    [Route("api/select")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly string password = "%juDf*2U%P@Qt4t";
        private readonly string username = "Asset-Selector+Global+Admin";
        private readonly HttpClient _httpClient;

        public DataController()
        {
            _httpClient = new HttpClient();
        }

        [HttpGet]
        public async Task<IActionResult> GetData()
        {
            // Replace "API_URL" with the actual URL of the third-party API you want to access
            string query = Request.QueryString.Value;

            var parsed = HttpUtility.ParseQueryString(query);
            var value = parsed["value"];

            var apiUrl =
                "https://author-p52252-e348051.adobeaemcloud.com/api/assets/KCM/website-assets.infinity.json?limit=50";

            var basicAuthenticationValue = Convert.ToBase64String(
                Encoding.ASCII.GetBytes($"{username}:{password}")
            );

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add(
                "Authorization",
                $"Basic {basicAuthenticationValue}"
            );

            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                // Process the data or return it as-is
                return Ok(data);
            }
            else
            {
                // Handle the error condition
                return StatusCode((int)response.StatusCode);
            }
        }
    }
}
