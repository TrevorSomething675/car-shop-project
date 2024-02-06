using MainTz.Web.ViewModels.ImageViewModels;

namespace MainTz.Web.ViewModels.CarViewModels
{
    public class CarsModelResponse
    {
        public int PageCount { get; set; }
        public int PageNumber { get; set; }
        public IEnumerable<CarResponse> Cars { get; set; }
    }
}