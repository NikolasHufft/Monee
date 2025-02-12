using System.Text.Json.Serialization;

namespace BaseLibrary.Entities
{
    public class OvertimeType : BaseEntity
    {
        // One to Many
        [JsonIgnore]
        public List<Overtime>? OverTimes { get; set; }
    }
}
