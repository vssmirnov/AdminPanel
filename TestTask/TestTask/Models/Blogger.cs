using System.Text.Json.Serialization;

namespace TestTask.Models
{
    /// <summary>
    /// Blogger 
    /// </summary>
    public class Blogger
    {
        /// <summary>
        /// Identifier
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }
        /// <summary>
        /// Short describe
        /// </summary>
        [JsonPropertyName("title")]                
        public string Title { get; set; }
        /// <summary>
        /// First name
        /// </summary>
        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }
        /// <summary>
        /// Last name
        /// </summary>
        [JsonPropertyName("lastName")]
        public string LastName { get; set; }
        /// <summary>
        /// Link to photo blogger
        /// </summary>
        [JsonPropertyName("picture")]
        public string Picture { get; set; }
    }
}
