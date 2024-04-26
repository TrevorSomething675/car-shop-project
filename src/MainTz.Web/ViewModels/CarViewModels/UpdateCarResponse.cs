using MainTz.Web.ViewModels.BrandViewModels;
using MainTz.Web.ViewModels.ManufacturerViewModels;

namespace MainTz.Web.ViewModels.CarViewModels
{
	public class UpdateCarResponse
	{
		public CarResponse Car { get; set; }
		public List<BrandResponse> BrandsResponse { get; set; }
		public List<ManufacturerResponse> ManufacturersResponse { get; set; }
	}
}
