using FluentValidation;
using MainTz.Web.ViewModels.AccountViewModels;

namespace MainTz.Web.Validators
{
    public class UpdateLoginUserValidator : AbstractValidator<UpdateLoginUserRequest>
    {
        public UpdateLoginUserValidator() 
        {
            RuleFor(updateForm => updateForm.Name).MinimumLength(8)
                .WithMessage("Логин должен содержать больше 7 знаков").NotEmpty();
            RuleFor(updateForm => updateForm.ConfirmName).Equal(updateForm => updateForm.Name);
        }
    }
}
