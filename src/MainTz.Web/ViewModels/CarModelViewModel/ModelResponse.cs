using MainTz.Web.ViewModels.BrandViewModels;
using MainTz.Web.ViewModels.CarViewModels;

namespace MainTz.Web.ViewModels.CarModelViewModel
{
    public class ModelResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<CarResponse> Cars { get; set; }

        public int BrandId { get; set; }
        public BrandResponse Brand { get; set; }
    }
}
