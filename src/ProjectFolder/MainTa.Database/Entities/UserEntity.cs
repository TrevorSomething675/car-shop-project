namespace MainTz.Database.Entities
{
    public class UserEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public int CarId { get; set; }
        public ICollection<CarEntity> Cars { get; set; }
        public int RoleId { get; set; }
        public ICollection<RoleEntity> Roles { get; set; }
        public int NotificationId { get; set; }
        public ICollection<NotificationEntity> Notifications { get; set; }
    }
}