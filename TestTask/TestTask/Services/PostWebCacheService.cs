using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using TestTask.Interfaces;

namespace TestTask.Services
{
    public class PostWebCacheService : WebCacheService, IPostWebCacheService
    {
        private readonly IPostService postService;
        private readonly ProtectedLocalStorage protectedLocalStorage;

        public PostWebCacheService(IPostService postService, ProtectedLocalStorage protectedLocalStorage)
        {
            this.postService = postService;
            this.protectedLocalStorage = protectedLocalStorage;
        }

        public async Task<IList<Models.Post>> GetListData(string cacheName, string id)
        {
            var cacheValues = await GetValueFromCache<Models.Post>(protectedLocalStorage, cacheName);

            if (cacheValues != null)
            {
                return cacheValues;
            }
            else
            {
                var resultRequest = await postService.GetPostsAsync(id);
                await SetCache(protectedLocalStorage, cacheName, resultRequest);
                return resultRequest;
            }
        }
    }
}
