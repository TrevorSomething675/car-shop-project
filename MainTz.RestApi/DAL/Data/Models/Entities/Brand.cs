using MainTz.RestApi.dal.Data;

namespace MainTz.RestApi.DAL.Data.Models.Entities
{
    public class Brand : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Model> Models{ get; set; }
    }
}
