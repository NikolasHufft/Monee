namespace BaseLibrary.Entities
{
    public class Bank : BaseEntity
    {
        // Relationship one-to-many        
        public virtual Town? Town { get; set; }
        public int TownId { get; set; }
    }
}
