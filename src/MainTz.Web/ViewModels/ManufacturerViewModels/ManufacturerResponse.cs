using MainTz.Web.ViewModels.CarViewModels;

namespace MainTz.Web.ViewModels.ManufacturerViewModels
{
    public class ManufacturerResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public List<CarRequest> Cars { get; set; }
    }
}
