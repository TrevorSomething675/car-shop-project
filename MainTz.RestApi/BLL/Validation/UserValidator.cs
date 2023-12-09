using MainTz.RestApi.DAL.Data.Models.DtoModels;
using FluentValidation;

namespace MainTz.RestApi.BLL.Validation
{
    public class UserValidator : AbstractValidator<UserDto>
    {
        public UserValidator() 
        {
            RuleFor(user => user.Name).NotNull().NotEmpty();
            RuleFor(user => user.Password).MinimumLength(8).MaximumLength(20).NotNull().NotEmpty();
        }
    }
}
