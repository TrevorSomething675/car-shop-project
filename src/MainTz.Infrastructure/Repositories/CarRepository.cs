using MainTz.Application.Repositories;
using Microsoft.EntityFrameworkCore;
using MainTz.Application.Models;
using MainTz.Database.Entities;
using MainTa.Database.Context;
using AutoMapper;

namespace MainTz.Infrastructure.Repositories
{
    /// <summary>
    /// Репозиторий CarRepository это обёртка на MainContext для таблицы Car
    /// </summary>
    public class CarRepository : ICarRepository
    {
        private readonly IDbContextFactory<MainContext> _dbContextFactory;
        private readonly IMapper _mapper;
        public CarRepository(IMapper mapper, IDbContextFactory<MainContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }
        public async Task<Car> GetCarByIdAsync(int id)
        {
            await using(var context = _dbContextFactory.CreateDbContext())
            {
                var carEntity = await context.Cars
                    .Include(car => car.Images)
                    .Include(car => car.Brand)
                    .Include(car => car.Description)
                    .FirstOrDefaultAsync(car => car.Id == id);
                var car = _mapper.Map<Car>(carEntity);
                return car;
            }
        }
		public async Task<Car> GetCarByNameAsync(string name)
        {
			await using (var context = _dbContextFactory.CreateDbContext())
			{
				var carEntity = await context.Cars
					.Include(car => car.Images)
					.Include(car => car.Brand)
                    .Include(car => car.Description)
                    .FirstOrDefaultAsync(car => car.Name == name);
				var car = _mapper.Map<Car>(carEntity);
				return car;
			}
		}
		public async Task<List<Car>> GetCarsAsync()
        {
            await using(var context = _dbContextFactory.CreateDbContext())
            {
                var carEntities = await context.Cars
                    .Include(car => car.Images)
                    .Where(car => car.IsVisible)
                    .Include(car => car.Description)
                    .ToListAsync();
                var cars = _mapper.Map<List<Car>>(carEntities);
                return cars;
            }
        }
		public async Task<List<Car>> GetCarsWithHiddenAsync()
        {
			await using (var context = _dbContextFactory.CreateDbContext())
			{
				var carEntities = await context.Cars
					.Include(car => car.Images)
                    .Include(car => car.Description)
					.ToListAsync();
				var cars = _mapper.Map<List<Car>>(carEntities);
				return cars;
			}
		}
		public async Task<Car> UpdateAsync(Car car)
		{
			await using (var context = _dbContextFactory.CreateDbContext())
			{
		        var updatedCarEntity = _mapper.Map<CarEntity>(car);
                var carEntity = context.Cars
                    .Include(c => c.Users)
                    .Include(c => c.Images)
                    .Include(c => c.Brand)
                    .Include(car => car.Description)
                    .FirstOrDefault(c => c.Id == updatedCarEntity.Id);

                carEntity.Name = updatedCarEntity.Name;
                carEntity.IsVisible = updatedCarEntity.IsVisible;
                carEntity.Description = updatedCarEntity.Description;
                carEntity.Price = updatedCarEntity.Price;

                if(carEntity.Images != null && updatedCarEntity.Images.FirstOrDefault().Name != null)
                {
                    carEntity.Images = updatedCarEntity.Images;
                    /*
                    if(carEntity.Images.Count() < updatedCarEntity.Images.Count())
                    {
                        foreach (var image in carEntity.Images)
                        {
                            var imageToRemove = updatedCarEntity.Images.FirstOrDefault(i => i.Id == image.Id);
                            updatedCarEntity.Images.Remove(imageToRemove);
                        }
                        carEntity.Images.AddRange(updatedCarEntity.Images);
                    } 
                    else if(carEntity.Images.Count() > updatedCarEntity.Images.Count())
                    {
                        var imageEntities = carEntity.Images.Select(i => i.Id).ToList();
                        foreach (var imageId in imageEntities)
                        {
                            var imageToRemove = updatedCarEntity.Images.FirstOrDefault(i => i.Id == imageId);
                            if(imageToRemove == null)
                            {
                                var imageEntityToRemove = carEntity.Images.FirstOrDefault(i => i.Id == imageId);
                                carEntity.Images.Remove(imageEntityToRemove);
                            }
                        }
                    } 
                    else if(carEntity.Images.Count() == updatedCarEntity.Images.Count())
                    {
                        if(updatedCarEntity.Images.FirstOrDefault().Name != null) 
                        {
                            foreach (var image in carEntity.Images)
                            {
                                image.Name = updatedCarEntity.Images[carEntity.Images.IndexOf(image)].Name;
                                image.Path = updatedCarEntity.Images[carEntity.Images.IndexOf(image)].Path;
                            }
                        }
                    }*/
                }
                if(carEntity.Brand != null)
                {
                    var dbCarBrand = context.Brands
                        .FirstOrDefault(m => m.Name == updatedCarEntity.Brand.Name);
                    carEntity.Brand = dbCarBrand;
                }
                context.Update(carEntity);
				await context.SaveChangesAsync();

				var updatedCar = _mapper.Map<Car>(carEntity);
				return updatedCar;
			}
		}
		public async Task<Car> CreateAsync(Car car)
        {
            await using (var context = _dbContextFactory.CreateDbContext())
            {
                var manufacturerEntity = context.Manufacturers.FirstOrDefault(m => m.Name == car.Manufacturer.Name);
                var brandEntity = context.Brands.FirstOrDefault(b => b.Name == car.Brand.Name);
                var carEntity = _mapper.Map<CarEntity>(car);

                if(manufacturerEntity != null)
                    carEntity.Manufacturer = manufacturerEntity;

                if(brandEntity != null)
                    carEntity.Brand = brandEntity;

                context.Cars.Add(carEntity);
                await context.SaveChangesAsync();

                var addedCarEntity = context.Cars.FirstOrDefault(car => car.Name == carEntity.Name);
                var addedCar = _mapper.Map<Car>(addedCarEntity);
                return addedCar;
            }
        }
        public async Task RemoveCarByIdAsync(int id)
        {
            await using(var context = _dbContextFactory.CreateDbContext())
            {
                var carEntity = context.Cars.FirstOrDefault(c => c.Id == id);
                context.Cars.Remove(carEntity);
                await context.SaveChangesAsync();
            }
        }
	}
}