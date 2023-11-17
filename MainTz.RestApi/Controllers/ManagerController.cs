using MainTz.RestApi.BLL.Services.Abstractions;
using MainTz.RestApi.dal.Data.Models.DtoModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MainTz.RestApi.Controllers
{
    public class ManagerController : Controller
	{
		private readonly ICarService _carService;

		public ManagerController(ICarService carService)
		{
			_carService = carService;
		}
		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> CreateCar(CarDto carDto)
		{
			try
			{
				await _carService.CreateCar(carDto);
				return Ok("Успешно добавлено");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpPost]
		public async Task<IActionResult> DeleteCar(CarDto carDto)
		{
			try
			{
				await _carService.DeleteCar(carDto);
				return Ok("Успешно удалено");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[Authorize]
		public async Task<IActionResult> GetInfo()
		{
			return Ok("Info data");
		}
	}
}
