using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BaseLibrary.Entities
{
    public class Employee : BaseEntity
    {
        [Required]
        public string CivilId { get; set; }
        [Required]
        public string FileNumber { get; set; }
        [Required]
        public string JobTitle { get; set; }
        //[Required]
        //public string LastName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required, DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required, DataType(DataType.ImageUrl)]
        public string PhotoUrl { get; set; }
        public string Notes { get; set; }

        // Relationships : Many to One
        //public City? City { get; set; }
        //public int CityId { get; set; }

        // Relationships : Many to One
        public virtual Branch? Branch { get; set; }
        public int BranchId { get; set; }

        public virtual Town? Town { get; set; }
        public int TownId { get; set; }

        //public int DoctorId { get; set; }
        //[JsonIgnore]
        //public ICollection<Doctor>? Doctors { get; set; } = [];
        [JsonIgnore]
        public virtual ICollection<EmployeeDoctor>? EmployeeDoctors { get; set; } = [];
        [JsonIgnore]
        public virtual ICollection<Overtime>? Overtimes { get; set; } = [];
        [JsonIgnore]
        public virtual ICollection<Sanction>? Sanctions { get; set; } = [];
        [JsonIgnore]
        public virtual ICollection<Vacation>? Vacations { get; set; } = [];
    }
}
