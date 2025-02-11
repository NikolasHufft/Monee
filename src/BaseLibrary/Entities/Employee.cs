using System.ComponentModel.DataAnnotations;

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
        [Required]
        public string FullName { get; set; }
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
        public Branch? Branch { get; set; }
        public int BranchId { get; set; }

        public Town? Town { get; set; }
        public int TownId { get; set; }
    }
}
