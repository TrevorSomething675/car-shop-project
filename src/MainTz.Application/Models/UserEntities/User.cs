using MainTz.Application.Models.CarEntities;
using MainTz.Database.Entities;

namespace MainTz.Application.Models.UserEntities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public int RoleId { get; set; }
		public Role Role { get; set; }

		public ICollection<Car> Cars { get; set; } = new List<Car>();
        public ICollection<Notification> Notifications { get; set; }
    }
}