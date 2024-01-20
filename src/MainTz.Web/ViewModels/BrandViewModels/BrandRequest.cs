using MainTz.Web.ViewModels.CarModelViewModel;

namespace MainTz.Web.ViewModels.BrandViewModels
{
    public class BrandRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ModelRequest> Models { get; set; }
    }
}