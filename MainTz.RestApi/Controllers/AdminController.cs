using MainTz.RestApi.BLL.Services.Abstractions;
using MainTz.RestApi.dal.Data.Models.Entities;
using MainTz.RestApi.DAL.Data.Models.DtoModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MainTz.RestApi.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IUsersService _usersService;
        //private readonly UserManager<User> _userManager;
        public AdminController(IUsersService usersService/*, UserManager<User> userManager*/)
        {
            //_userManager = userManager;
            _usersService = usersService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _usersService.GetUsers();

            return View(model);
            //return View(model);
        }

		public async Task<IActionResult> Create(UserDto userDto)
        {
            try
            {
                await _usersService.CreateUser(userDto);
                return Ok("Успешно");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
