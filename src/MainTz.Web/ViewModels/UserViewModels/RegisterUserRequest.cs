namespace MainTz.Web.ViewModels.UserViewModels
{
    public class RegisterUserRequest : LoginUserRequest
    {
        public string Email { get; set; }
        public string ConfirmPassword { get; set; }
    }
}