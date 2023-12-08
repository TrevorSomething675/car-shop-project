using MainTz.RestApi.dal.Data;

namespace MainTz.RestApi.DAL.Data.Models.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public int CarId { get; set; }
        public ICollection<Car> Cars { get; set; }
        public int RoleId { get; set; }
        public ICollection<Role> Roles { get; set; } 
        public int NotificationId { get; set; }
        public ICollection<Notification> Notifications { get; set; }
    }
}