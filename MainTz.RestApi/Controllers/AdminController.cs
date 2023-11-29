using MainTz.RestApi.BLL.Services.Abstractions;
using MainTz.RestApi.DAL.Data.Models.DtoModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MainTz.RestApi.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IUsersService _usersService;

        public AdminController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _usersService.GetUsers();

            return View(model);
        }

        public async Task<IActionResult> CreateUser(UserDto userDto)
        {
            var result = await _usersService.Create(userDto);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(UserDto userDto)
        {
            var result = await _usersService.Delete(userDto);
            return RedirectToAction("Index");
		}

        public async Task<IActionResult> UpdateUser(UserDto userDto)
        {
            var result = await _usersService.Update(userDto);
			return RedirectToAction("Index");
        }

        public async Task<IActionResult> GetTokenByRefreshToken()
        {
            return RedirectToAction("GetTokenOnRefresh", "Auth");
        }
    }
}
