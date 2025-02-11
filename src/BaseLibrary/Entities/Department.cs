using System.Text.Json.Serialization;

namespace BaseLibrary.Entities
{
    public class Department : BaseEntity
    {
        // Many to one relationship with GeneralDepartment
        public int GeneralDepartmentId { get; set; }
        public GeneralDepartment? GeneralDepartment { get; set; }

        // One to many relationship with Branch
        public List<Branch>? Branches { get; set; }
    }
}
