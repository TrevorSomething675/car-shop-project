namespace MainTz.Web.ViewModels
{
    public class LoginUserRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
		public string Password { get; set; }

		public string Role { get; set; }
    }
}
