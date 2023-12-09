using MainTz.RestApi.dal.Data;

namespace MainTz.RestApi.DAL.Data.Models.Entities
{
    public class Image : BaseEntity
    {
        public string Name { get; set; }
        public byte[] File { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
    }
}
