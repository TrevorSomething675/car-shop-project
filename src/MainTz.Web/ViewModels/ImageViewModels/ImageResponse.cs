using MainTz.Web.ViewModels.CarViewModels;

namespace MainTz.Web.ViewModels.ImageViewModels
{
    public class ImageResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string FileBase64String { get; set; }
        public int CarId { get; set; }
        public CarResponse Car { get; set; }
    }
}