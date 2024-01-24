using MainTz.Web.ViewModels.NotificationViewModels;
using MainTz.Web.ViewModels.CarViewModels;
using MainTz.Web.ViewModels.RoleViewModels;

namespace MainTz.Web.ViewModels.UserViewModels
{
    public class UserResponse
    {
		public int Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public RoleResponse Role { get; set; }

		public IEnumerable<CarResponse> Cars { get; set; }
		public IEnumerable<NotificationResponse> Notifications { get; set; }
	}
}