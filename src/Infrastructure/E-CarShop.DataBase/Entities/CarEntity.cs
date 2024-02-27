namespace E_CarShop.DataBase.Entities
{
    public class CarEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public bool IsVisible { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }

        public List<ImageEntity> Images { get; set; }
        public List<UserEntity> Users { get; set; }
        public int ModelId { get; set; }
        public BrandEntity Brand { get; set; }
    }
}