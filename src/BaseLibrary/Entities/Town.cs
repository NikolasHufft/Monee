using System.Text.Json.Serialization;

namespace BaseLibrary.Entities
{
    public class Town : BaseEntity
    {
        // Relationships : one-to-many
        //[JsonIgnore]
        //public List<User>? Users { get; set; }
        [JsonIgnore]
        public virtual ICollection<Employee>? Employees { get; set; }

        // Relationships : Many to One
        public virtual City? City { get; set; }
        public int CityId { get; set; }
    }
}
