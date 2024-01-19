namespace MainTz.Web.ViewModels
{
    public class RegisterUserRequest : LoginUserRequest
    {
		public string Email { get; set; }
		public string ConfirmPassword { get; set; }
	}
}