using MainTz.Web.ViewModels.BrandViewModels;
using MainTz.Web.ViewModels.CarViewModels;

namespace MainTz.Web.ViewModels.CarModelViewModel
{
    public class ModelRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<CarRequest> Cars { get; set; }

        public int BrandId { get; set; }
        public BrandRequest Brand { get; set; }
    }
}
