namespace MainTz.Application.Models.CarEntities
{
    public class Image
    {
        public string Name { get; set; }
        public byte[] File { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
    }
}