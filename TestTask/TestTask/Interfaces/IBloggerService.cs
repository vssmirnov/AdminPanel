using TestTask.Models;

namespace TestTask.Interfaces
{
    /// <summary>
    /// Http service of blogger
    /// </summary>
    public interface IBloggerService
    {
        /// <summary>
        /// Getting bloggers from http request
        /// </summary>
        /// <returns>List bloggers</returns>
        Task<IList<Blogger>> GetBloggersAsync();
    }
}