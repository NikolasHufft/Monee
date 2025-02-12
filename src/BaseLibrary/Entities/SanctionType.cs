using System.Text.Json.Serialization;

namespace BaseLibrary.Entities
{
    public class SanctionType : BaseEntity
    {
        // One to Many
        [JsonIgnore]
        public List<Sanction>? Sanctions { get; set; }
    }
}
