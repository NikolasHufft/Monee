using System.Text.Json.Serialization;

namespace BaseLibrary.Entities
{
    public class Branch : BaseEntity
    {
        // Relationships : Many to one
        public Department? Department { get; set; }
        public int DepartmentId { get; set; }

        // Relationships : one-to-many
        [JsonIgnore]
        public virtual ICollection<Employee>? Employees { get; set; }
    }
}
