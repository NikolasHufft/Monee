namespace BaseLibrary.Entities
{
    public class Country : BaseEntity
    {
        // One to Many
        public List<City> Cities { get; set; } // = new List<City>();


    }
}
