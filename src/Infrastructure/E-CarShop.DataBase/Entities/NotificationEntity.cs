namespace E_CarShop.DataBase.Entities
{
    public class NotificationEntity : BaseEntity
    {
        public bool IsRead { get; set; }
        public DateTime SendedDate { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }

        public int UserId { get; set; }
        public UserEntity User { get; set; }
    }
}