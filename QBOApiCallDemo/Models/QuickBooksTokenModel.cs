namespace QBOApiCallDemo.Models
{
    public class QuickBooksTokenModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpirationTime { get; set; }
        // Add other properties as needed
    }

}
