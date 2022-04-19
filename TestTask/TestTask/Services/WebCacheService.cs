using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace TestTask.Services
{
    public class WebCacheService
    {
        private const int lifeBuffer = 1;

        public async Task<IList<T>> GetData<T>(ProtectedLocalStorage store, string cacheName, Func<Task<IList<T>>> getData){
            
            var cacheValues = await GetValueFromCache<T>(store, cacheName);

            if (cacheValues != null)
            {
                return cacheValues;
            }
            else
            {
                IList<T> resultRequest = await getData();
                await SetCache(store, cacheName, resultRequest);
                return resultRequest;
            }
        }

        public async Task<IList<T>> GetData<T>(ProtectedLocalStorage store, string cacheName, Func<string, Task<IList<T>>> getData, string param)
        {
            
            var cacheValues = await GetValueFromCache<T>(store, cacheName);

            if (cacheValues != null)
            {
                return cacheValues;
            }
            else
            {
                IList<T> resultRequest = await getData(param);
                await SetCache(store, cacheName, resultRequest);
                return resultRequest;
            }
        }

        private static async Task SetCache<T>(ProtectedLocalStorage store, string cacheName, IList<T> resultRequest)
        {
            await store.DeleteAsync(cacheName);
            var cacheData = new CachedData<T>() { Data = resultRequest, GetDateTime = DateTime.Now.AddMinutes(lifeBuffer) };
            await store.SetAsync(cacheName, cacheData);
        }

        private async Task<IList<T>?> GetValueFromCache<T>(ProtectedLocalStorage store, string cacheName)
        {
            var result = await store.GetAsync<CachedData<T>>(cacheName);

            bool isValidCache = result.Success && result.Value != null && result.Value.GetDateTime > DateTime.Now;
            return isValidCache ? result.Value.Data : null;
        }

        private class CachedData<T>
        {
            public IList<T> Data { get; set; }
            public DateTime GetDateTime { get; set; }
        }
    }
}
