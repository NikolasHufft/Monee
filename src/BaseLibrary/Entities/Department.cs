using System.Text.Json.Serialization;

namespace BaseLibrary.Entities
{
    public class Department : BaseEntity
    {
        // Relationships : one-to-many
        [JsonIgnore]
        public List<User>? Users { get; set; }
    }
}
