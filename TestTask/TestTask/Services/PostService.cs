using TestTask.Models;
using TestTask.Interfaces;

namespace TestTask.Services
{
    public class PostService : BaseRestService<Post>, IPostService
    {
        private string baseUrl;

        public PostService(string apiKey, string baseUrl, IHttpClientFactory httpClientFactory) : base(apiKey, httpClientFactory)
        {
            this.baseUrl = baseUrl;
        }

        public async Task<IList<Post>> GetPostsAsync(string Id)
        {
            string requestUri = baseUrl + $"/user/{Id}/post?limit=10";

            return await GetRequest(requestUri);
        }
    }
}
