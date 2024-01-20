using FluentValidation;
using MainTz.Web.ViewModels.AccountViewModels;

namespace MainTz.Web.Validators
{
    public class UpdatePasswordUserValidator : AbstractValidator<UpdatePasswordUserRequest>
    {
        public UpdatePasswordUserValidator() 
        {
            RuleFor(updateForm => updateForm.NewPassword)
                .Matches("[A-Z]").WithMessage("Пароль должен содержать заглавные буквы.")
                .Matches("[a-z]").WithMessage("Пароль должен содержать строчные буквы.")
                .MinimumLength(8).MaximumLength(20).NotNull().NotEmpty();

            RuleFor(updateFrom => updateFrom.ConfirmNewPassword)
                .Equal(updateFrom => updateFrom.NewPassword)
                .WithMessage("Пароли не совпадают.");
        }
    }
}
