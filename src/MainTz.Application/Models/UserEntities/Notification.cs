namespace MainTz.Application.Models.UserEntities
{
    public class Notification
    {
        public bool IsRead { get; set; }
        public DateTime SendedDate { get; set; }
        public string Description { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
