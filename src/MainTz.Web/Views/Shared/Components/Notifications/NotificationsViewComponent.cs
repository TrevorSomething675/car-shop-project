using MainTz.Web.ViewModels.UserViewModels;
using MainTz.Application.Services;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using MainTz.Web.ViewModels;

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
            var contextUser = _httpContextAccessor.HttpContext.User.Identity.Name;
            var user = await _userService.GetUserByNameAsync(contextUser);
            var userResponse = _mapper.Map<UserResponse>(user);
            var notificationsCount = userResponse.Notifications.Take(5).Count();

            var model = new NotificationsModel
            {
                Notifications = userResponse.Notifications,
                NotificationsCount = notificationsCount
            };

            return View(model);
        }
    }
}
