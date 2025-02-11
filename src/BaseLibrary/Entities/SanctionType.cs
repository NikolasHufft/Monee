namespace BaseLibrary.Entities
{
    public class SanctionType : BaseEntity
    {
        // One to Many
        public List<Sanction>? Sanctions { get; set; }
    }
}
