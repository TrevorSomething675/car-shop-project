using MainTz.Web.ViewModels.UserViewModels;
using MainTz.Application.Services;
using Microsoft.AspNetCore.Mvc;
using MainTz.Web.ViewModels;
using FluentValidation;
using AutoMapper;
using MainTz.Web.ViewModels.NotificationViewModels;
using MainTz.Web.ViewModels.AccountViewModels;

namespace MainTz.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IValidator<UpdatePasswordUserRequest> _updatePasswordFormValidator;
        private readonly IValidator<UpdateLoginUserRequest> _updateLoginFormValidator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly INotificationService _notificationService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public AccountController(IUserService userService, IHttpContextAccessor httpContextAccessor, IMapper mapper, 
            INotificationService notificationService, IValidator<UpdateLoginUserRequest> updateLoginFormValidator,
            IValidator<UpdatePasswordUserRequest> updatePasswordFormValidator)
        {
            _updatePasswordFormValidator = updatePasswordFormValidator;
            _updateLoginFormValidator = updateLoginFormValidator;
            _httpContextAccessor = httpContextAccessor;
            _notificationService = notificationService;
            _userService = userService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userService.GetUserByNameAsync(_httpContextAccessor?.HttpContext?.User?.Identity?.Name);
            var model = _mapper.Map<UserResponse>(user);

            return View(model);
        }
        public async Task<IActionResult> GetNotifications()
        {
            var user = await _userService.GetUserByNameAsync(_httpContextAccessor.HttpContext.User.Identity.Name);
            var notifications = await _notificationService.GetNotificationsByUserAsync(user);
            var notificationsResponse = _mapper.Map<List<NotificationResponse>>(notifications);

            var model = new NotificationsModel
            {
                NotificationsCount = notifications.Count(),
                NewNotifications = notificationsResponse.Where(notif => notif.IsRead == false),
                LegacyNotifications = notificationsResponse.Where(notif => notif.IsRead == true)
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> GetNotificationDescription([FromBody]int id)
        {
            var contextUserName = _httpContextAccessor.HttpContext.User.Identity.Name;
            var user = await _userService.GetUserByNameAsync(contextUserName);
            var notification = await _notificationService.GetNotificationByIdAndUserWithMarkedAsync(user, id);
            var notificationResponse = _mapper.Map<NotificationResponse>(notification);

            return PartialView("GetNotificationDescription", notificationResponse);
        }
        [HttpPost]
        public async Task<IResult> UpdateLoginUserAccount(UpdateLoginUserRequest updateLoginUserRequest)
        {
            var requestForm = _updateLoginFormValidator.Validate(updateLoginUserRequest);
            if (!requestForm.IsValid)
                return Results.BadRequest(new ErrorViewModel { ErrorMessage = string.Join(" ", requestForm.Errors.Select(err => err.ErrorMessage)) });

            var user = await _userService.GetUserByNameAsync(_httpContextAccessor.HttpContext.User.Identity.Name);
            user.Name = updateLoginUserRequest.NewName;
            var result = await _userService.UpdateAsync(user);

            if (result)
                return Results.Ok();
            else
                return Results.BadRequest(new ErrorViewModel { ErrorMessage = "Критическая ошибка" });
        }
        [HttpPost]
        public async Task<IResult> UpdatePasswordUserAccount(UpdatePasswordUserRequest updatePasswordUserRequest)
        {
            var requestForm = _updatePasswordFormValidator.Validate(updatePasswordUserRequest);
            if (!requestForm.IsValid)
                return Results.Json(new ErrorViewModel { ErrorMessage = string.Join(" ", requestForm.Errors.Select(err => err.ErrorMessage)) });
            var user = await _userService.GetUserByNameAsync(_httpContextAccessor.HttpContext.User.Identity.Name);
            user.Password = updatePasswordUserRequest.NewPassword;
            var result = await _userService.UpdateAsync(user);

            if(result)
                return Results.Ok();
            else
                return Results.BadRequest(new ErrorViewModel { ErrorMessage = "Критическая ошибка" });
        }
    }
}