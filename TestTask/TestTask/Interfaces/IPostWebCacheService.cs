using TestTask.Models;

namespace TestTask.Interfaces
{
    /// <summary>
    /// Wrapper for caching http response on website
    /// </summary>
    public interface IPostWebCacheService
    {
        /// <summary>
        /// Retrieving posts from the cache within 1 minute
        /// </summary>
        /// <param name="cacheName">Generic cache name</param>
        /// <param name="id">Identifier of blogger</param>
        /// <returns>List posts</returns>
        Task<IList<Post>> GetListData(string cacheName, string id);
    }
}