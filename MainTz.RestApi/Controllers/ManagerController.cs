using MainTz.RestApi.BLL.Services.Abstractions;
using MainTz.RestApi.dal.Data.Models.DtoModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MainTz.RestApi.Controllers
{
	[Authorize(Roles = "Manager")]
    public class ManagerController : Controller
	{
		private readonly ICarService _carService;

		public ManagerController(ICarService carService)
		{
			_carService = carService;
		}
		public async Task<IActionResult> Index()
		{
			var model = await _carService.GetCars();
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> CreateCar(CarDto carDto)
		{
			var result = await _carService.CreateCar(carDto);
			return RedirectToAction("Index");
		}

		[HttpPost]
		public async Task<IActionResult> DeleteCar(CarDto carDto)
		{
			var result = await _carService.DeleteCar(carDto);
			return RedirectToAction("Index");
		}

		[HttpPost]
		public async Task<IActionResult> UpdateCar(CarDto carDto)
		{
			var result = await _carService.UpdateCar(carDto);
			return RedirectToAction("Index");
		}
	}
}