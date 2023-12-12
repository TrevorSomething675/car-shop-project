﻿namespace MainTz.Database.Entities
{
    public class UserEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public ICollection<CarEntity>? Cars { get; set; }

        public int RoleId { get; set; }
        public RoleEntity Role { get; set; }

        public ICollection<NotificationEntity>? Notifications { get; set; }
    }
}