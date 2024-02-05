using MainTz.Web.ViewModels.CarModelViewModel;
using MainTz.Web.ViewModels.BrandViewModels;
using MainTz.Web.ViewModels.ImageViewModels;

namespace MainTz.Web.ViewModels.CarViewModels
{
	public class UpdateCarResponse
	{
		public CarResponse Car { get; set; }
		public ImageResponse Image { get; set; }
		public List<BrandResponse> BrandsResponse { get; set; }
		public List<ModelResponse> ModelsResponse { get; set; }
	}
}
