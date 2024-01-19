namespace MainTz.Application.Models.CarModels
{
    public class Model
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Car> Cars { get; set; }

        public int BrandId { get; set; }
        public Brand Brand { get; set; }
    }
}