namespace MainTz.Web.ViewModels.CarViewModels
{
    public class CarsViewModel
    {
        public int PageNumber { get; set; }
        public IEnumerable<CarResponse> CarsResponse { get; set; }
    }
}
