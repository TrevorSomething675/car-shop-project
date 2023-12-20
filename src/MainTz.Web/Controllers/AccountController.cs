using AutoMapper;
using MainTz.Application.Services;
using MainTz.Web.ViewModels;
using MainTz.Web.ViewModels.UserViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MainTz.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public AccountController(IUserService userService, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetNotifications()
        {
            var contextUserName = _httpContextAccessor.HttpContext.User.Identity.Name;
            var user = await _userService.GetUserByNameAsync(contextUserName);
            var userResponse = _mapper.Map<UserResponse>(user);
            var notificationsCount = userResponse.Notifications.Count();

            var model = new NotificationsModel
            {
                Notifications = userResponse.Notifications.ToList(),
                NotificationsCount = notificationsCount
            };

            return View(model);
        }
        public async Task<IActionResult> GetNotificationById(int id)
        {
            var contextUserName = _httpContextAccessor.HttpContext.User.Identity.Name;
            var user = await _userService.GetUserByNameAsync(contextUserName);
            var userResponse = _mapper.Map<UserResponse>(user);
            var notificationModel = user.Notifications.FirstOrDefault(notif => notif.Id == id);
            var notificationResponse = _mapper.Map<NotificationResponse>(notificationModel);

            return PartialView(notificationResponse);
        }
    }
}
