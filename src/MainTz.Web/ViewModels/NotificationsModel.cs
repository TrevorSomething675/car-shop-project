using MainTz.Web.ViewModels.UserViewModels;

namespace MainTz.Web.ViewModels
{
    public class NotificationsModel
    {
        public int NotificationsCount { get; set; }
        public IEnumerable<NotificationResponse> Notifications { get; set; }
    }
}
