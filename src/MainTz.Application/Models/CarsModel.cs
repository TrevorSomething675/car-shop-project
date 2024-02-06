namespace MainTz.Application.Models
{
    public class CarsModel
    {
        public int? PageCount { get; set; }
        public int? PageNumber { get; set; }
        public IEnumerable<Car> Cars { get; set; }
    }
}