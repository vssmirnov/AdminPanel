namespace TestTask.Models
{
    /// <summary>
    /// Account
    /// </summary>
    public class User
    {
        /// <summary>
        /// Identifier
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Username for logging
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// Password for logging
        /// </summary>
        public string Password { get; set; }
    }
}
