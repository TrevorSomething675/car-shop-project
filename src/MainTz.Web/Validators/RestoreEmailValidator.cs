using MainTz.Web.ViewModels;
using FluentValidation;

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
