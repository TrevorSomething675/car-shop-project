using MainTz.Web.ViewModels;
using FluentValidation;

namespace MainTz.Web.Validators
{
	public class LoginFormValidator : AbstractValidator<LoginUserRequest>
	{
		public LoginFormValidator() 
		{
			RuleFor(logForm => logForm.Name).NotEmpty();
			RuleFor(logForm => logForm.Password).NotEmpty();
		}
	}
}