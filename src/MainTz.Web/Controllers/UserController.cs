using MainTz.Application.Models.UserEntities;
using MainTz.Web.ViewModels.UserViewModels;
using Microsoft.AspNetCore.Authorization;
using MainTz.Application.Services;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace MainTz.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetUsersAsync();
            var userResponse = _mapper.Map<List<UserResponse>>(users);

            return View(userResponse);
        }
        public async Task<IActionResult> CreateUser(UserRequest userRequest)
        {
            var user = _mapper.Map<User>(userRequest);
            var result = await _userService.CreateAsync(user);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> DeleteUser(UserRequest userRequest)
        {
            var user = _mapper.Map<User>(userRequest);
            var result = await _userService.DeleteAsync(user);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> UpdateUser(UserRequest userRequest)
        {
            var user = _mapper.Map<User>(userRequest);
            var result = await _userService.UpdateAsync(user);
            return RedirectToAction("Index");
        }
    }
}