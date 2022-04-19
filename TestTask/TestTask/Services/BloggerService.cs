using System.Text.Json;
using TestTask.Models;

namespace TestTask.Services
{
    public class BloggerService: BaseRestService<Blogger>
    {
        private string baseUrl;

        public BloggerService(string apiKey, string baseUrl, IHttpClientFactory httpClientFactory): base(apiKey, httpClientFactory)
        {
            this.baseUrl = baseUrl;
        }

        public async Task<IList<Blogger>> GetBloggersAsync()
        {
            string requestUri = baseUrl + "/user?limit=10";

            return await GetRequest(requestUri);
        }
    }
}
