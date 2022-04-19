using System.Text.Json;
using System.Text.Json.Serialization;

namespace TestTask.Services
{
    public abstract class BaseRestService<T>
    {
        private string apiKey;
        private readonly IHttpClientFactory httpClientFactory;

        protected BaseRestService(string apiKey, IHttpClientFactory httpClientFactory)
        {
            this.apiKey = apiKey;
            this.httpClientFactory = httpClientFactory;
        }

        protected async Task<List<T>> GetRequest(string requestUri)
        {
            using (var handler = new HttpClientHandler())
            {
                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri)
                {
                    Headers = {
                        { "app-id", apiKey }
                    }
                };

                var httpClient = httpClientFactory.CreateClient("ignoreSSL");
                try
                {
                    using var result = await httpClient.SendAsync(httpRequestMessage);

                    if (result.IsSuccessStatusCode)
                    {
                        using var contentStream = await result.Content.ReadAsStreamAsync();

                        var resultRequest = await JsonSerializer.DeserializeAsync<HttpModel<T>>(contentStream);

                        return resultRequest != null ? resultRequest.Data : new List<T>();
                    }
                } catch (Exception ex) {
                    Console.WriteLine(ex);
                }                                
            }

            return new List<T>();
        }

        class HttpModel<T>
        {
            [JsonPropertyName("data")]
            public List<T> Data { get; set; }
        }

    }
}
