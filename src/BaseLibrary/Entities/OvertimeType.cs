using System.Text.Json.Serialization;

namespace BaseLibrary.Entities
{
    public class OvertimeType : BaseEntity
    {
        // One to Many
        [JsonIgnore]
        public virtual ICollection<Overtime>? OverTimes { get; set; }
    }
}
