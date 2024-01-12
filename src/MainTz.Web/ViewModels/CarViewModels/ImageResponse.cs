using MainTz.Application.Models.CarEntities;

namespace MainTz.Web.ViewModels.CarViewModels
{
    public class ImageResponse
    {
        public string Name { get; set; }
        public byte[] File { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
    }
}