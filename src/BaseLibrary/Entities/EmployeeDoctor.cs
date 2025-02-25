using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BaseLibrary.Entities
{
    //[PrimaryKey(nameof(EmployeeId), nameof(DoctorId))]
    public class EmployeeDoctor
    {
        [ForeignKey(nameof(Employee))]
        [Required]
        public int EmployeeId { get; set; }
        [JsonIgnore]
        public Employee? Employee { get; set; }

        [ForeignKey(nameof(Doctor))]
        [Required]
        public int DoctorId { get; set; }

        [JsonIgnore]
        public Doctor? Doctor { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

       
        
    }
}
