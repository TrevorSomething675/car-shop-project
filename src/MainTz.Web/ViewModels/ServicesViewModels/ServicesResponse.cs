using MainTz.Web.ViewModels.ManufacturerViewModels;
using MainTz.Web.ViewModels.BrandViewModels;

namespace MainTz.Web.ViewModels.ServicesViewModels
{
    public class ServicesResponse
    {
        public List<BrandResponse> Brands { get; set; }
        public List<ManufacturerResponse> Manufacturers { get; set; }
    }
}