using MainTz.Web.ViewModels.UserViewModels;

namespace MainTz.Web.ViewModels
{
    public class NotificationsModel
    {
        public int NotificationsCount { get; set; }
        public ICollection<NotificationResponse> Notifications { get; set; }
    }
}
