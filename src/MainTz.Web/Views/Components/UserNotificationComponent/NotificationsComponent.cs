using MainTz.Web.ViewModels.ViewComponentsModels;
using MainTz.Application.Services;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using MainTz.Web.ViewModels.UserViewModels;

namespace MainTz.Web.Views.Components.UserNotificationComponent
{
    public class NotificationsComponent : ViewComponent
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public NotificationsComponent(IHttpContextAccessor httpContextAccessor, IUserService userService, IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            //var contextUser = _httpContextAccessor.HttpContext.User.Identity.Name;
            //var user = await _userService.GetUserByNameAsync(contextUser);
            //var userResponse = _mapper.Map<UserResponse>(user);

            //var model = new NotificationsComponentModel
            //{
            //    User = userResponse
            //};

            return View(/*model*/);
        }
    }
}
