using System.Text.Json;
using TestTask.Models;

namespace TestTask.Services
{
    public class PostService
    {
        private string apiKey;
        private string baseUrl;

        public PostService(string apiKey, string baseUrl)
        {
            this.apiKey = apiKey;
            this.baseUrl = baseUrl;
        }

        public async Task<IList<Post>> GetPostsAsync(string Id)
        {
            using (var handler = new HttpClientHandler())
            {
                handler.ClientCertificateOptions = ClientCertificateOption.Manual;
                handler.ServerCertificateCustomValidationCallback =
                    (httpRequestMessage, cert, cetChain, policyErrors) =>
                    {
                        return true;
                    };

                using var client = new HttpClient(handler);
                client.DefaultRequestHeaders.Add("app-id", apiKey);

                using var result = await client.GetAsync(baseUrl + $"/user/{Id}/post?limit=10");

                if (result.IsSuccessStatusCode)
                {
                    using var contentStream =
                        await result.Content.ReadAsStreamAsync();

                    var posts = await JsonSerializer.DeserializeAsync<PostHttpModel>(contentStream);

                    return posts.data.ToList();
                }
            }

            return new List<Post>();
        }
    }

    class PostHttpModel
    {
        public IEnumerable<Post> data { get; set; }
    }
}
