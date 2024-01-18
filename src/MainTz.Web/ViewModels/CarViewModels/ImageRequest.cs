namespace MainTz.Web.ViewModels.CarViewModels
{
    public class ImageRequest
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string FileBase64String { get; set; }
        public int CarId { get; set; }
        public CarRequest Car { get; set; }
    }
}