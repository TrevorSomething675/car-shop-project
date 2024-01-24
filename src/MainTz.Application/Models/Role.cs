namespace MainTz.Application.Models
{
    public class Role
    {
        public int Id { get; set; }
        public ICollection<User> Users { get; set; }
        public string RoleName { get; set; }
    }
}
