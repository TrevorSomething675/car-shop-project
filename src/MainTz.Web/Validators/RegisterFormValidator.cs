using MainTz.Web.ViewModels;
using FluentValidation;

namespace MainTz.Web.Validators
{
	public class RegisterFormValidator : AbstractValidator<RegisterFormRequest>
	{
		public RegisterFormValidator()
		{
			RuleFor(regForm => regForm.Name).NotEmpty();
			RuleFor(regForm => regForm.Email).EmailAddress().NotEmpty();
			RuleFor(regForm => regForm.Password)
				.Matches("[A-Z]").WithMessage("Пароль должен содержать заглавные буквы")
				.Matches("[a-z]").WithMessage("Пароль должен содержать строчные буквы")
				.MinimumLength(8).MaximumLength(20).NotEmpty();
			RuleFor(regForm => regForm.ConfirmPassword).Equal(regForm => regForm.Password).NotEmpty();
		}
	}
}