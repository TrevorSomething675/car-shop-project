namespace MainTz.Application.Models.CarModels
{
    public class Image
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string FileBase64String { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
    }
}