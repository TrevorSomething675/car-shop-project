using MainTz.Web.ViewModels.CarModelViewModel;
using MainTz.Web.ViewModels.BrandViewModels;

namespace MainTz.Web.ViewModels.CarViewModels
{
    public class CreateCarResponse
    {
        public List<BrandResponse> BrandsResponse { get; set; }
        public List<ModelResponse> ModelsResponse { get; set; }
    }
}