using MainTz.Application.Repositories;
using Microsoft.EntityFrameworkCore;
using MainTz.Database.Entities;
using MainTa.Database.Context;
using System.Linq.Expressions;

namespace MainTz.Infrastructure.Repositories
{
    /// <summary>
    /// Репозиторий CarRepository это обёртка на MainContext для таблицы Car
    /// </summary>
    public class CarRepository : ICarRepository
    {
        private readonly MainContext _context;
        public CarRepository(MainContext context)
        {
            _context = context;
        }

        public async Task<CarEntity> GetCarByIdAsync(int id)
        {
            var car = await _context.Cars.FirstOrDefaultAsync(car => car.Id == id);
            return car;
        }

        public async Task<List<CarEntity>> GetCarsAsync(Expression<Func<CarEntity, bool>> filter = null)
        {
            filter = filter ?? (car => true);

            var cars = await _context.Cars.Where(filter).ToListAsync();
            return cars;
        }

        public async Task CreateAsync(CarEntity car)
        {
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(CarEntity car)
        {
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CarEntity car)
        {
            _context.Cars.Update(car);
            await _context.SaveChangesAsync();
        }
    }
}