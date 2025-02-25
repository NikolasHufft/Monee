using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BaseLibrary.Entities
{
    public class City : BaseEntity
    {
        //// Relationships : one-to-many
        //[JsonIgnore]
        //public List<User>? Users { get; set; }

        // Relationships : Many to One
        public virtual Country? Country { get; set; }
        [Required]
        public int CountryId { get; set; }

        // Relationships : One to Many
        [JsonIgnore]
        public virtual ICollection<Town>? Towns { get; set; } // = new List<Town>();
    }
}
