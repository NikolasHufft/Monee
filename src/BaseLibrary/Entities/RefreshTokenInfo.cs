namespace BaseLibrary.Entities
{
    public class RefreshTokenInfo
    {
        public int Id { get; set; }
        public string? Token { get; set; }
        public int UserId { get; set; }
        public string? JwtId { get; set; }
        public bool IsUsed { get; set; }
        public bool IsRevoked { get; set; } = false;
        public System.DateTime AddedDate { get; set; }
        public System.DateTime ExpiryDate { get; set; }
    }
}
