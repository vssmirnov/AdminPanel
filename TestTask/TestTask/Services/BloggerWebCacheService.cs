using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using TestTask.Interfaces;

namespace TestTask.Services
{
    public class BloggerWebCacheService : WebCacheService, IBloggerWebCacheService
    {
        private readonly IBloggerService postService;
        private readonly ProtectedLocalStorage protectedLocalStorage;

        public BloggerWebCacheService(IBloggerService postService, ProtectedLocalStorage protectedLocalStorage)
        {
            this.postService = postService;
            this.protectedLocalStorage = protectedLocalStorage;
        }

        public async Task<IList<Models.Blogger>> GetListData(string cacheName)
        {
            var cacheValues = await GetValueFromCache<Models.Blogger>(protectedLocalStorage, cacheName);

            if (cacheValues != null)
            {
                return cacheValues;
            }
            else
            {
                var resultRequest = await postService.GetBloggersAsync();
                await SetCache(protectedLocalStorage, cacheName, resultRequest);
                return resultRequest;
            }
        }
    }
}
