using System.Text.Json.Serialization;

namespace BaseLibrary.Entities
{
    public class VacationType : BaseEntity
    {
        // Many-to-one
        [JsonIgnore]
        public virtual ICollection<Vacation>? Vacations { get; set; }
    }
}
