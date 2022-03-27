using System.Text.Json;
using TestTask.Models;

namespace TestTask.Services
{
    public class PostService
    {
        private string apiKey;
        private string baseUrl;
        private readonly IHttpClientFactory _httpClientFactory;

        public PostService(string apiKey, string baseUrl, IHttpClientFactory httpClientFactory)
        {
            this.apiKey = apiKey;
            this.baseUrl = baseUrl;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IList<Post>> GetPostsAsync(string Id)
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, baseUrl + $"/user/{Id}/post?limit=10")
            {
                Headers = { { "app-id", apiKey }
            }
            };

            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream =
                    await httpResponseMessage.Content.ReadAsStreamAsync();

                var posts = await JsonSerializer.DeserializeAsync<PostHttpModel>(contentStream);

                return posts.data.ToList();
            }

            return new List<Post>();
        }
    }
    class PostHttpModel
    {
        public IEnumerable<Post> data { get; set; }
    }
}
