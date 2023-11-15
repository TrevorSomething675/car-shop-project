using MainTz.RestApi.Repositories.Abstractions;
using MainTz.RestApi.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MainTz.RestApi.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly MainContext _context;
        private readonly ILogger<CarRepository> _logger;
        public CarRepository(MainContext context, ILogger<CarRepository> logger) 
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Car> GetCar(Expression<Func<Car, bool>> filter)
        {
            var car = await _context.Cars.FirstOrDefaultAsync(filter);
            return car;
        }

        public async Task<List<Car>> GetCars(Expression<Func<Car, bool>> filter = null)
        {
            filter = filter ?? (car => true);

            var cars = await _context.Cars.Where(filter).ToListAsync();
            return cars;
        }

        public async Task Create(Car car)
        {
            try
            {
                _context.Cars.Add(car);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
				_logger.LogInformation($"{ex.Message}");
			}
        }

        public async Task Update(Car car)
        {
            try
            {
                _context.Cars.Update(car);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
				_logger.LogInformation($"{ex.Message}");
			}
        }

        public async Task Delete(Car car)
        {
            try
            {
                _context.Cars.Remove(car);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
				_logger.LogInformation($"{ex.Message}");
			}
        }
    }
}
