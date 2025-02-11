namespace BaseLibrary.Entities
{
    public class OvertimeType : BaseEntity
    {
        // One to Many
        public List<Overtime>? OverTimes { get; set; }
    }
}
