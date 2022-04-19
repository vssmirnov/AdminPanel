using TestTask.Models;

namespace TestTask.Interfaces
{
    /// <summary>
    /// Interface authorization of user
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Getted account user if he was authorization
        /// </summary>
        /// <param name="username">Account username</param>
        /// <param name="password">Account password</param>
        /// <returns>Account user</returns>
        Task<User> LoginAsync(string username, string password);
    }
}