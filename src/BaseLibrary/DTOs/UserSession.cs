using System.Text.Json.Serialization;

namespace BaseLibrary.DTOs
{
    public class UserSession
    {
        [JsonPropertyName("Token")]
        public string? Token { get; set; }
        [JsonPropertyName("RefreshToken")]
        //[Json]
        public string? RefreshToken { get; set; }
    }
}
