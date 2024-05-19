using Microsoft.EntityFrameworkCore;
using MainTz.Application.Services;
using MainTz.Application.Models;
using MainTz.Database.Entities;
using Microsoft.AspNetCore.Mvc;
using MainTa.Database.Context;
using AutoMapper;

namespace MainTz.Web.Controllers
{
	public class ImageController : Controller
	{
		private readonly IMapper _mapper;
		private readonly IMinioService _minioService;
		private readonly IDbContextFactory<MainContext> _dbContextFactory;
		public ImageController(IDbContextFactory<MainContext> dbContextFactory, 
			IMapper mapper, IMinioService minioService) 
		{
			_mapper = mapper;
			_minioService = minioService;
			_dbContextFactory = dbContextFactory;
		}
		[HttpPost]
		public async Task<IActionResult> RemoveImageFromCar([FromBody] int imageId)
		{
			using (var context = _dbContextFactory.CreateDbContext())
			{
				var imageEntity = context.Images.FirstOrDefault(i => i.Id == imageId);
                if (imageEntity != null)
				{
					var result = context.Remove(imageEntity);
					await context.SaveChangesAsync();
					return Ok(result.Entity.Id);
				}
				return BadRequest();
			}
		}
		public async Task<IResult> CreateImageFromCar(Image image, int carId)
		{
			using(var context = _dbContextFactory.CreateDbContext())
			{
				var imageToCreate = _mapper.Map<ImageEntity>(image);
				var carEntity = context.Cars.FirstOrDefault(car => car.Id == carId);
				imageToCreate.Path = await _minioService.CreateObjectAsync(image);
				if (carEntity != null)
				{
					imageToCreate.Car = carEntity;
					context.Images.Add(imageToCreate);
					context.SaveChanges();
					return Results.Ok(carEntity.Id);
				}
				return Results.BadRequest();
			}
		}
	}
}
