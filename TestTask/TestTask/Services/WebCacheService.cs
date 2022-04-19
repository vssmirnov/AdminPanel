using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace TestTask.Services
{
    public abstract class WebCacheService
    {
        protected virtual int lifeBuffer { get; set; } = 1;        

        public async Task<IList<T>> GetListData<T>(ProtectedLocalStorage store, string cacheName, Func<Task<IList<T>>> getData)
        {
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

        protected async Task SetCache<T>(ProtectedLocalStorage store, string cacheName, IList<T> resultRequest)
        {
            try
            {
                await store.DeleteAsync(cacheName);
                var cacheData = new CachedData<T>() { Data = resultRequest, GetDateTime = DateTime.Now.AddMinutes(lifeBuffer) };
                await store.SetAsync(cacheName, cacheData);
            }            
            catch (Exception ex)
            {
                Console.WriteLine(ex);                
            }
        }

        protected async Task<IList<T>?> GetValueFromCache<T>(ProtectedLocalStorage store, string cacheName)
        {
            try
            {
                var result = await store.GetAsync<CachedData<T>>(cacheName);
                bool isValidCache = result.Success && result.Value != null && result.Value.GetDateTime > DateTime.Now;
                return isValidCache ? result.Value.Data : null;
            } catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new List<T>();
            }
        }

        private class CachedData<T>
        {
            public IList<T> Data { get; set; }
            public DateTime GetDateTime { get; set; }
        }
    }
}
