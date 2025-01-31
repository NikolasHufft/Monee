using System.Text.Json.Serialization;

namespace BaseLibrary.Entities
{
    public class City : BaseEntity
    {
        // Relationships : one-to-many
        [JsonIgnore]
        public List<User>? Users { get; set; }
    }
}
