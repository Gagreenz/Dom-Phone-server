namespace Dom_Phone_server.Models.Data
{
    public class RefreshToken
    {
        public int ID { get; set; }
        public string Token { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiredAt { get; set; }
    }
}
