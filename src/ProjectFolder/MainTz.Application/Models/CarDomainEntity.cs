namespace MainTz.Application.Models
{
    public class CarDomainEntity
    {
        public int? Id { get; set; }
        public string Brand { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string Color { get; set; } = null!;
    }
}
