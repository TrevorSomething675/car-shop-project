using MainTz.RestApi.BLL.Services.Abstractions;
using MainTz.RestApi.DAL.Data.Models.DtoModels;
using Microsoft.AspNetCore.Mvc;

namespace MainTz.RestApi.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IUsersService _usersService;
        public AdminController(ILogger<AdminController> logger, IUsersService usersService) 
        {
			_usersService = usersService;
            _logger = logger;
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
