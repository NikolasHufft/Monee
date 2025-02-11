namespace BaseLibrary.Entities
{
    public class Town : BaseEntity
    {
        // Relationships : one-to-many
        public List<User>? Users { get; set; }
        
        // Relationships : Many to One
        public City? City { get; set; }
        public int CityId { get; set; }
    }
}
