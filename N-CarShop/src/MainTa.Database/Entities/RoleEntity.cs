namespace MainTz.Database.Entities
{
    public class RoleEntity
    {
        public string RoleName { get; set; }
        public int UserId { get; set; }
        public UserEntity User { get; set; }
    }
}