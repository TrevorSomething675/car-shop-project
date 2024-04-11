namespace MainTz.Database.Entities
{
    public class ManufacturerEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public List<CarEntity> Cars { get; set; }
    }
}