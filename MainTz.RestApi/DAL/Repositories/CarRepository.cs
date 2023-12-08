using MainTz.RestApi.DAL.Repositories.Abstractions;
using MainTz.RestApi.DAL.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace MainTz.RestApi.DAL.Repositories
{
	/// <summary>
	/// Репозиторий CarRepository это обёртка на MainContext для таблицы Car
	/// </summary>
	public class CarRepository : ICarRepository
    {
        private readonly MainContext _context;
        private readonly ILogger<CarRepository> _logger;
        public CarRepository(MainContext context, ILogger<CarRepository> logger)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Car> GetCarById(int id)
        {
            var car = await _context.Cars.FirstOrDefaultAsync(car => car.Id == id);
            return car; 
        }

        public async Task<List<Car>> GetCars()
        {
            var cars = await _context.Cars.ToListAsync();
            return cars;
        } 

        public async Task Create(Car car)
        {
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Car car)
        {
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Car car)
        {
            _context.Cars.Update(car);
            await _context.SaveChangesAsync();
        }
    }
}
