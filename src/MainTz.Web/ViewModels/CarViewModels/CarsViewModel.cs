namespace MainTz.Web.ViewModels.CarViewModels
{
    public class CarsViewModel
    {
        public int pageNumber { get; set; }
        public IEnumerable<CarResponse> CarsResponse { get; set; }
    }
}
