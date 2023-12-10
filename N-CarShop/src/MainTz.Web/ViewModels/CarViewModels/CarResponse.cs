namespace MainTz.Web.ViewModels.CarViewModels
{
    public class CarResponse
    {
        public int? Id { get; set; }
        public string Brand { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string Color { get; set; } = null!;
    }
}
