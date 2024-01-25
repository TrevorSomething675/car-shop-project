using MainTz.Web.ViewModels.UserViewModels;
using Microsoft.AspNetCore.Authorization;
using MainTz.Application.Services;
using MainTz.Application.Models;
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
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetUsersAsync();
            var userResponse = _mapper.Map<List<UserResponse>>(users);
            var model = new UsersViewModel()
            {
                PageCount = 8,
                PageNumber = 1,
                UsersResponse = userResponse
            };
            return View(model);
        }
        public async Task<IActionResult> ChangeUserRole([FromBody]int id)
        {
            var user = await _userService.ChangeRoleForUserByIdAsync(id);
            var userResponse = _mapper.Map<UserResponse>(user);
            return PartialView("GetUserPartial", userResponse);
        }
        public async Task<IActionResult> GetUserPartial(UserResponse userResponse)
        {
            return PartialView(userResponse);
        }
        [HttpPost]
        public async Task<IActionResult> GetUsersPartial([FromBody]GetUsersRequest getUsersRequest)
        {
            if (getUsersRequest.IsSortedByRole)
            {
                var sortedUsers = await _userService.GetSortedUsersByRole();
                var sortedUsersResponse = _mapper.Map<List<UserResponse>>(sortedUsers);
                return PartialView(sortedUsersResponse);
            }
            else
            {
                var users = await _userService.GetUsersAsync();
                var userResponse = _mapper.Map<List<UserResponse>>(users);
                return PartialView(userResponse);
            }
        }
        public async Task<IActionResult> CreateUser(UserRequest userRequest)
        {
            var user = _mapper.Map<User>(userRequest);
            var result = await _userService.CreateAsync(user);
            return RedirectToAction("Index");
        }
    }
}