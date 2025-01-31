namespace BaseLibrary.Entities
{
    public class Emploee : BaseEntity
    {
        public string CivilId { get; set; }
        public string FileNumber { get; set; }
        public string JobTitle { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string? PhoneNumber { get; set; }

        public string Email { get; set; }  

        public string PhotoUrl { get; set; }
        public string Notes { get; set; }

        // Relationships : Many to One
        public Department? Department { get; set; }
        public int DepartmentId { get; set; }

        public City? City { get; set; }
        public int CityId { get; set; }
    }
}
