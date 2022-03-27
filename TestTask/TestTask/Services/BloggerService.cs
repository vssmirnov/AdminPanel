using System.Text.Json;
using TestTask.Models;

namespace TestTask.Services
{
    public class BloggerService
    {
        private string apiKey;
        private string baseUrl;

        public BloggerService(string apiKey, string baseUrl)
        {
            this.apiKey = apiKey;
            this.baseUrl = baseUrl;
        }

        public async Task<IList<Blogger>> GetBloggersAsync()
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
                
                using var result = await client.GetAsync(baseUrl + "/user?limit=10");
                
                if (result.IsSuccessStatusCode)
                {
                    using var contentStream =
                        await result.Content.ReadAsStreamAsync();

                    var bloggers = await JsonSerializer.DeserializeAsync<bloggerHttpModel>(contentStream);

                    return bloggers.data.ToList();
                }
            }                

            return new List<Blogger>();
        }
    }

    class bloggerHttpModel
    {
        public IEnumerable<Blogger> data { get; set; }
    }
}
