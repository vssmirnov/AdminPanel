using TestTask.Models;

namespace TestTask.Services
{
    public class PostService
    {
        public async Task<IList<Post>> GetPostsAsync()
        {
            return await Task.Run(() => new List<Post>() { new Post {
                id = "60d21b4667d0d8992e610c85",
                image = "https://img.dummyapi.io/photo-1564694202779-bc908c327862.jpg",
                likes = 43,                
                text = "adult Labrador retriever",
                publishDate = "2020-05-24T14:53:17.598Z",
            }});
        }
    }
}
