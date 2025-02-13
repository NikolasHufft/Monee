using System.Text.Json.Serialization;

namespace BaseLibrary.Entities
{
    public class GeneralDepartment : BaseEntity
    {
        // One to many relationship with Department
        [JsonIgnore]
        public virtual ICollection<Department>? Departments { get; set; }
    }
}
