using System.Text.Json.Serialization;

namespace BaseLibrary.Entities
{
    public class SanctionType : BaseEntity
    {
        // One to Many
        [JsonIgnore]
        public virtual ICollection<Sanction>? Sanctions { get; set; }
    }
}
