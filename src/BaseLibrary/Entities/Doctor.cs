using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BaseLibrary.Entities
{
    public class Doctor
    {
        public int Id { get; set; }
        public string? CiviId { get; set; }
        public string? FileNumber { get; set; }
        public string? Other { get; set; }

        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string MedicalDiagnose { get; set; } = string.Empty;
        [Required]
        public string MedicalRecommendation { get; set; } = string.Empty;
        public string MedicalPrescription { get; set; } = string.Empty;
        public string MedicalCertificate { get; set; } = string.Empty;
        [Required, DataType(DataType.DateTime), JsonIgnore]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Relationships : one-to-many
        //[JsonIgnore]
        //public virtual List<Employee> Employees { get; set; } = [];
        //public int EmployeeId { get; set; }
        //[JsonIgnore]
        //public virtual ICollection<Employee>? Employee { get; set; } = [];
        [JsonIgnore]
        public virtual ICollection<EmployeeDoctor>? EmployeeDoctors { get; set; } = [];
    }
}
