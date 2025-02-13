using System.Text.Json.Serialization;

namespace BaseLibrary.Entities
{
    public class Country : BaseEntity
    {
        // One to Many
        [JsonIgnore]
        public virtual ICollection<City>? Cities { get; set; } // = new List<City>();
    }
}
