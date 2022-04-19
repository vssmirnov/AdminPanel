using TestTask.Models;

namespace TestTask.Interfaces
{
    /// <summary>
    /// Wrapper for caching http response on website
    /// </summary>
    public interface IBloggerWebCacheService
    {
        /// <summary>
        /// Retrieving bloggers from the cache within 1 minute
        /// </summary>
        /// <param name="cacheName">Generic cache name</param>
        /// <returns>List bloggers</returns>
        Task<IList<Blogger>> GetListData(string cacheName);
    }
}