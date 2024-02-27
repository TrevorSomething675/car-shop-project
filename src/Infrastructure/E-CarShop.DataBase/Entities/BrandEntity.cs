namespace E_CarShop.DataBase.Entities
{
    public class BrandEntity
    {
        public string Name { get; set; }
        public ICollection<CarEntity> Cars { get; set; }
    }
}