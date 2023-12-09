using MainTz.RestApi.dal.Data;

namespace MainTz.RestApi.DAL.Data.Models.Entities
{
    public class Model : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Car> Cars { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
    }
}
