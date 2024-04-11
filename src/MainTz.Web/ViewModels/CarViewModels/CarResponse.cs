using MainTz.Web.ViewModels.BrandViewModels;
using MainTz.Web.ViewModels.ImageViewModels;
using MainTz.Web.ViewModels.UserViewModels;

namespace MainTz.Web.ViewModels.CarViewModels
{
    public class CarResponse
    {
        public int? Id { get; set; }
        public string Name { get; set; }
		public string Color { get; set; }
		public bool IsVisible { get; set; }
		public string Description { get; set; }
        public int Price { get; set; }

		public IEnumerable<ImageResponse> Images { get; set; }

		public IEnumerable<UserResponse> Users { get; set; }

		public int BrandId { get; set; }
		public BrandResponse Brand { get; set; }
		public int ManufacturerId { get; set; }
	}
}