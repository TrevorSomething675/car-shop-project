using MainTz.Web.ViewModels.CarModelViewModel;
using System.Text.Json.Serialization;

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
        public IEnumerable<int> ImagesId { get; set; }
		[JsonIgnore]
		public IEnumerable<int> UsersId { get; set; }

		public int ModelId { get; set; }
        public ModelResponse Model { get; set; }
	}
}