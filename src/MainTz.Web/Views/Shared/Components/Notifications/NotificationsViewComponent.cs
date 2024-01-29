using MainTz.Web.ViewModels.NotificationViewModels;
using MainTz.Web.ViewModels.UserViewModels;
using MainTz.Application.Services;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace MainTz.Web.Views.Shared.Components.Notifications
{
    public class NotificationsViewComponent : ViewComponent
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public NotificationsViewComponent(IHttpContextAccessor httpContextAccessor, IUserService userService, IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userId = Convert.ToInt32(_httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(u => u.Type == "Id")?.Value);
            var user = await _userService.GetUserByIdAsync(userId);
            var userResponse = _mapper.Map<UserResponse>(user);
            var notifications = userResponse.Notifications.Where(notif => notif.IsRead == false);
            var notificationsCount = notifications.Take(5).Count();

            var model = new NotificationsModel
            {
                LegacyNotifications = notifications,
                NotificationsCount = notificationsCount
            };

            return View(model);
        }
    }
}
