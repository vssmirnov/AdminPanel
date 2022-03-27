using TestTask.Models;

namespace TestTask.Services
{
    public class BloggerService
    {
        public async Task<IList<Blogger>> GetBloggersAsync()
        {
            return await Task.Run(() => new List<Blogger>() { new Blogger {id = "60d0fe4f5311236168a109ca",
                title = "ms",
                firstName = "Sara",
                lastName = "Andersen",
                picture = "https://randomuser.me/api/portraits/women/58.jpg"
            }});
        }
    }
}
