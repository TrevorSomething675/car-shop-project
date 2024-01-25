using MainTz.Web.ViewModels.NotificationViewModels;
using MainTz.Web.ViewModels.CarViewModels;

namespace MainTz.Web.ViewModels.UserViewModels
{
    public class UserRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public IEnumerable<CarRequest> Cars { get; set; }
        public IEnumerable<NotificationRequest> Notifications { get; set; }
    }
}