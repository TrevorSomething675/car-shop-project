using MainTz.Application.Models.UserEntities;

namespace MainTz.Application.Models.CarModels
{
    public class Car
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public bool IsVisible { get; set; }
        public bool IsFavorite { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }

        public IEnumerable<int> ImagesId { get; set; }
        public IEnumerable<Image> Images { get; set; }

        public IEnumerable<int> UsersId { get; set; }
        public IEnumerable<User> Users { get; set; } = new List<User>();

        public int ModelId { get; set; }
        public Model Model { get; set; }
    }
}
