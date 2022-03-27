using System.Text.Json;
using TestTask.Models;

namespace TestTask.Services
{
    public class BloggerService
    {
        private string apiKey;
        private string baseUrl;
        private readonly IHttpClientFactory _httpClientFactory;

        public BloggerService(string apiKey, string baseUrl, IHttpClientFactory httpClientFactory)
        {
            this.apiKey = apiKey;
            this.baseUrl = baseUrl;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IList<Blogger>> GetBloggersAsync()
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, baseUrl+ "/user?limit=10")
            {
                Headers = { { "app-id", apiKey }
            }};

            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream =
                    await httpResponseMessage.Content.ReadAsStreamAsync();

                var bloggers = await JsonSerializer.DeserializeAsync<bloggerHttpModel>(contentStream);

                return bloggers.data.ToList();
            }

            return new List<Blogger>();
        }
    }

    class bloggerHttpModel
    {
        public IEnumerable<Blogger> data { get; set; }
    }
}
