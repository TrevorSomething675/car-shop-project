using MainTz.Web.ViewModels.CarModelViewModel;

namespace MainTz.Web.ViewModels.BrandViewModels
{
    public class BrandResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ModelResponse> Models { get; set; }
    }
}