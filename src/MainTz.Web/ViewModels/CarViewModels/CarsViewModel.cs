namespace MainTz.Web.ViewModels.CarViewModels
{
    public class CarsViewModel
    {
        public int PageCount { get; set; }
        public int PageNumber { get; set; }
        public IEnumerable<CarResponse> CarsResponse { get; set; }
    }
}