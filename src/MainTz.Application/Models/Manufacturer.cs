namespace MainTz.Application.Models
{
    public class Manufacturer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public List<Car> Cars { get; set; }
    }
}
