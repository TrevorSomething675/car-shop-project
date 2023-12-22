using MainTz.Web.ViewModels.UserViewModels;
using MainTz.Application.Services;
using Microsoft.AspNetCore.Mvc;
using MainTz.Web.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace MainTz.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly INotificationService _notificationService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public AccountController(IUserService userService, IHttpContextAccessor httpContextAccessor, IMapper mapper, INotificationService notificationService)
        {
            _httpContextAccessor = httpContextAccessor;
            _notificationService = notificationService;
            _userService = userService;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
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
        public async Task<IActionResult> GetNotificationHeaders()
        {

            return PartialView();
        }
    }
}
