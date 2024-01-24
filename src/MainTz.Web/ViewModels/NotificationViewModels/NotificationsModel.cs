namespace MainTz.Web.ViewModels.NotificationViewModels
{
    public class NotificationsModel
    {
        public int NotificationsCount { get; set; }
        public IEnumerable<NotificationResponse> NewNotifications { get; set; }
        public IEnumerable<NotificationResponse> LegacyNotifications { get; set; }
    }
}