using MainTz.Web.ViewModels.NotificationViewModels;
using MainTz.Web.ViewModels.AccountViewModels;
using MainTz.Web.ViewModels.UserViewModels;
using MainTz.Web.ViewModels.CarViewModels;
using MainTz.Application.Services;
using MainTz.Application.Models;
using Microsoft.AspNetCore.Mvc;
using MainTz.Web.ViewModels;
using FluentValidation;
using AutoMapper;
using MainTz.Core.Enums;

namespace MainTz.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IValidator<UpdatePasswordUserRequest> _updatePasswordFormValidator;
        private readonly IValidator<UpdateLoginUserRequest> _updateLoginFormValidator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly INotificationService _notificationService;
        private readonly IUserService _userService;
        private readonly ICarService _carService;
        private readonly IMapper _mapper;
        public AccountController(IUserService userService, IHttpContextAccessor httpContextAccessor, IMapper mapper, 
            INotificationService notificationService, IValidator<UpdateLoginUserRequest> updateLoginFormValidator,
            IValidator<UpdatePasswordUserRequest> updatePasswordFormValidator, ICarService carService)
        {
            _updatePasswordFormValidator = updatePasswordFormValidator;
            _updateLoginFormValidator = updateLoginFormValidator;
            _httpContextAccessor = httpContextAccessor;
            _notificationService = notificationService;
            _userService = userService;
            _carService = carService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var userId = Convert.ToInt32(_httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(u => u.Type == "Id")?.Value);
            var user = await _userService.GetUserByIdAsync(userId);
            var model = _mapper.Map<UserResponse>(user);

            return View(model);
        }
        public async Task<IActionResult> GetNotifications()
        {
            var userId = Convert.ToInt32(_httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(u => u.Type == "Id")?.Value);
            var user = await _userService.GetUserByIdAsync(userId);
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
            var userId = Convert.ToInt32(_httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(u => u.Type == "Id")?.Value);
            var user = await _userService.GetUserByIdAsync(userId);
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

            var userId = Convert.ToInt32(_httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(u => u.Type == "Id")?.Value);
            var user = _mapper.Map<User>(updateLoginUserRequest);

            var result = await _userService.UpdateUserData(user, userId);

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

            var userId = Convert.ToInt32(_httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(u => u.Type == "Id")?.Value);
            //var user = await _userService.GetUserByIdAsync(userId);

            //user.Password = updatePasswordUserRequest.Password;
            var user = _mapper.Map<User>(updatePasswordUserRequest);
            var result = await _userService.UpdateUserData(user, userId);

            if(result)
                return Results.Ok();
            else
                return Results.BadRequest(new ErrorViewModel { ErrorMessage = "Критическая ошибка" });
        }
        public async Task<IResult> SendNotificationForUsersOnCarId(NotificationFormRequest notificationFormRequest) 
        {
            var notification = _mapper.Map<Notification>(notificationFormRequest.Notification);
            var result = await _notificationService.SendNotificationOnCarIdWithDescription(notificationFormRequest.Id, notification);
            if (result)
                return Results.Ok();
            else
                return Results.BadRequest(new ErrorViewModel { ErrorMessage = "Не удалось отправить уведомления пользователям" });
        }
        public async Task<IActionResult> GetSendManualNotification(int id)
        {
            var car = await _carService.GetCarByIdAsync(id);
            var carResponse = _mapper.Map<CarResponse>(car);
            return View(carResponse);
        }
    }
}