namespace MainTz.Web.ViewModels.UserViewModels
{
    public class UserResponse
    {
		public int Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string Role { get; set; }

		public ICollection<int> CarsId { get; set; }
		public IEnumerable<NotificationResponse> Notifications { get; set; }
	}
}