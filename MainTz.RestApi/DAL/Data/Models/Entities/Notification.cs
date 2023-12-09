using MainTz.RestApi.dal.Data;

namespace MainTz.RestApi.DAL.Data.Models.Entities
{
    public class Notification : BaseEntity
    {
        public bool IsRead { get; set; }
        public DateTime SendedDate { get; set; }
        public string Description { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}