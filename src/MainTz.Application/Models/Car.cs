namespace MainTz.Application.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public bool IsVisible { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }

        public IEnumerable<Image> Images { get; set; }

        public IEnumerable<User> Users { get; set; }

        public int BrandId { get; set; }
        public Brand Brand { get; set; }
    }
}
