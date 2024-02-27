namespace E_CarShop.DataBase.Entities
{
    public class UserEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public int RoleId { get; set; }
        public RoleEntity Role { get; set; }

        public List<CarEntity> Cars { get; set; }
        public List<NotificationEntity>? Notifications { get; set; }
    }
}