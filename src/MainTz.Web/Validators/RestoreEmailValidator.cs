using FluentValidation;
using MainTz.Web.ViewModels.AccountViewModels;

namespace MainTz.Web.Validators
{
    public class RestoreEmailValidator : AbstractValidator<RestoreEmailRequest>
    {
        public RestoreEmailValidator()
        {
            RuleFor(restoreForm => restoreForm.Email).EmailAddress().NotEmpty();
        }
    }
}
