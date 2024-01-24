using Microsoft.AspNetCore.Mvc;
using MainTz.Web.ViewModels;
using FluentValidation;
using MainTz.Web.ViewModels.AccountViewModels;

namespace MainTz.Web.Controllers
{
    public class MailController : Controller
    {
        private readonly IValidator<RestoreEmailRequest> _retoreEmailValidator;
        public MailController(IValidator<RestoreEmailRequest> retoreEmailValidator)
        {
            _retoreEmailValidator = retoreEmailValidator;
        }

        [HttpGet]
        public async Task<IActionResult> RestoreAccountOnEmail()
        {
            return View();
        }
        [HttpPost]
        public async Task<IResult> RestoreAccountOnEmail(RestoreEmailRequest restoreEmailRequest)
        {
            var restoreEmailValidator = _retoreEmailValidator.Validate(restoreEmailRequest);
            if (!restoreEmailValidator.IsValid)
                return Results.Json(new ErrorViewModel { ErrorMessage = "Неверный адрес почты" });

            //var isValidEmail = await _mailService.CheckEmailAsync("soalone999@mail.ru");
            return Results.Json(new MessageModel { Message = "Если эта почта ассоциирована с пользователем, то на нее была отправлена инструкция по сбросу" });
        }
    }
}
