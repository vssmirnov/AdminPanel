using TestTask.Models;

namespace TestTask.Interfaces
{
    /// <summary>
    /// Http service of posts of blogger
    /// </summary>
    public interface IPostService
    {
        /// <summary>
        /// Getting posts from http request
        /// </summary>
        /// <param name="Id">Identifer of blogger</param>
        /// <returns>List posts of blogger</returns>
        Task<IList<Post>> GetPostsAsync(string Id);
    }
}