using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.Entities
{
    public class Vacation
    {
        public int Id { get; set; }
        public string? CiviId { get; set; }
        public string? FileNumber { get; set; }
        public string? Other { get; set; }
        [Required, DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }
        [Required, DataType(DataType.DateTime)]
        public DateTime EndtDate => StartDate.AddDays(NumberOfDays);
        public int NumberOfDays => (EndtDate - StartDate).Days;
        public double Average { get; set; }
        public double Amount { get; set; }
        public DateTime CreatedAt { get; set; }

        //  Many-to-one relationship
        public int VacationTypeId { get; set; }
        public virtual VacationType? VacationType { get; set; }
    }
}
