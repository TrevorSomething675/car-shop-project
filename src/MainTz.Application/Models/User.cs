namespace MainTz.Application.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }

        public List<Car> Cars { get; set; } = new List<Car>();
        public List<Notification> Notifications { get; set; }
    }
}