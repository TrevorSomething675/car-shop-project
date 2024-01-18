namespace MainTz.Web.ViewModels.CarViewModels
{
    public class CarRequest
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public bool IsVisible { get; set; }
        public bool IsFavorite { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }

        public IEnumerable<int> ImagesId { get; set; }
        public IEnumerable<ImageRequest> Images { get; set; }

        public IEnumerable<int> UsersId { get; set; }

        public int ModelId { get; set; }
    }
}