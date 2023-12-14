namespace MainTz.Web.ViewModels
{
    public class RegisterFormRequest : LoginFormRequest
    {
		public string Email { get; set; }
		public string ConfirmPassword { get; set; }
	}
}