using System.Text.Json.Serialization;

namespace BaseLibrary.Entities
{
    public class City : BaseEntity
    {
        //// Relationships : one-to-many
        //[JsonIgnore]
        //public List<User>? Users { get; set; }

        // Relationships : Many to One
        public List<Country>? Country { get; set; }
        public int CountryId { get; set; }

        // Relationships : One to Many
        [JsonIgnore]
        public List<Town>? Towns { get; set; } // = new List<Town>();
    }
}
