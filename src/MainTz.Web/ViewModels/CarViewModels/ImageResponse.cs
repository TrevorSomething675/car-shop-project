namespace MainTz.Web.ViewModels.CarViewModels
{
    public class ImageResponse
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string FileBase64String { get; set; }
        public int CarId { get; set; }
        public CarResponse Car { get; set; }
    }
}