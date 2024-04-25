namespace MainTz.Database.Entities
{
    public class CarEntity : BaseEntity
    {
        public string Name { get; set; }
        public bool IsVisible { get; set; }
        public int Price { get; set; }

        public List<ImageEntity> Images { get; set; }
        public List<UserEntity> Users { get; set; }

        public int BrandId { get; set; }
        public BrandEntity Brand { get; set; }

        public int ManufacturerId { get; set; }
        public ManufacturerEntity Manufacturer { get; set; }

        public DescriptionEntity Description { get; set; }
    }
}