namespace MainTz.Application.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string FileBase64String { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
    }
}