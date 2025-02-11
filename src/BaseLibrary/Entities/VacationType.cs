namespace BaseLibrary.Entities
{
    public class VacationType : BaseEntity
    {
        // Many-to-one
        public List<Vacation>? Vacations { get; set; }
    }
}
