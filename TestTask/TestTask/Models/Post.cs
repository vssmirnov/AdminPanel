using System.Text.Json.Serialization;

namespace TestTask.Models
{
    /// <summary>
    /// Post of blogger
    /// </summary>
    public class Post
    {
        /// <summary>
        /// Identifier
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }
        /// <summary>
        /// Link to main image this post
        /// </summary>
        [JsonPropertyName("image")]
        public string Image { get; set; }
        /// <summary>
        /// Amount likes of this post
        /// </summary>
        [JsonPropertyName("likes")]
        public int Likes { get; set; }
        /// <summary>
        /// Short describing this post
        /// </summary>
        [JsonPropertyName("text")]
        public string Text { get; set; }
        /// <summary>
        /// Publishing date of post
        /// </summary>
        [JsonPropertyName("publishDate")]
        public string PublishDate { get; set; }
    }
}
