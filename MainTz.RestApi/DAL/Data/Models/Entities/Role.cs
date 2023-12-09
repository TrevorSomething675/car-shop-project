using MainTz.RestApi.dal.Data;

namespace MainTz.RestApi.DAL.Data.Models.Entities
{
    public class Role : BaseEntity
    {
        public string RoleName { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}