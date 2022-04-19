using TestTask.Models;

namespace TestTask.Services
{
    public class AuthService
    {
        public async Task<User> LoginAsync(string username, string password)
        {
            var admin = new User
            {
                Username = "admin",
                Password = "admin"
            };

            if (admin.Password != password)
                return null;

            return admin;
        }
    }
}