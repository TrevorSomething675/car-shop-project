namespace MainTz.RestApi.Data.Models.Entities
{
    public class Car : BaseEntity
    {
        public string Brand { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string Color { get; set; } = null!;
    }
}
