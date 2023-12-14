using MainTz.Web.ViewModels;
using FluentValidation;

namespace MainTz.Web.Validators
{
	public class LoginFormValidator : AbstractValidator<LoginFormRequest>
	{
		public LoginFormValidator() 
		{
			RuleFor(logForm => logForm.Name).NotEmpty();
			RuleFor(logForm => logForm.Email)/*.EmailAddress()*/.NotEmpty();
			//RuleFor(logForm => logForm.Password)
			//	.Matches("[A-Z]").WithMessage("Пароль должен содержать заглавные буквы")
			//	.Matches("[a-z]").WithMessage("Пароль должен содержать строчные буквы")
			//	.MinimumLength(8).MaximumLength(20).NotEmpty();
		}
	}
}