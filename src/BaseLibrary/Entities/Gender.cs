using System.Text.Json.Serialization;

namespace BaseLibrary.Entities
{
    public class Gender : BaseEntity
    {
        // Relationships : one-to-many
        [JsonIgnore]
        public virtual ICollection<Employee>? Employees { get; set; }
    }
}
