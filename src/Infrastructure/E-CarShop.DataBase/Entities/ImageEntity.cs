namespace E_CarShop.DataBase.Entities
{
    public class ImageEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public int CarId { get; set; }
        public CarEntity Car { get; set; }
    }
}
