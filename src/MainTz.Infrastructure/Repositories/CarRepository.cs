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
        private readonly IDbContextFactory<MainContext> _dbContextFactory;
        public CarRepository(IDbContextFactory<MainContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public async Task<CarEntity> GetCarByIdAsync(int id)
        {
            using(var context = _dbContextFactory.CreateDbContext())
            {
                var car = await context.Cars.FirstOrDefaultAsync(car => car.Id == id);
                return car;
            }
        }
        public async Task<List<CarEntity>> GetCarsAsync(Expression<Func<CarEntity, bool>> filter = null)
        {
            filter = filter ?? (car => true);
            using(var context = _dbContextFactory.CreateDbContext())
            {
                var cars = await context.Cars.Where(filter).ToListAsync();
                return cars;
            }
        }
        public async Task CreateAsync(CarEntity car)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                context.Cars.Add(car);
                await context.SaveChangesAsync();
            }
        }
        public async Task DeleteAsync(CarEntity car)
        {
            using(var context = _dbContextFactory.CreateDbContext())
            {
                context.Cars.Remove(car);
                await context.SaveChangesAsync();
            }
        }
        public async Task UpdateAsync(CarEntity car)
        {
            using(var context = _dbContextFactory.CreateDbContext())
            {
                context.Cars.Update(car);
                await context.SaveChangesAsync();
            }
        }
    }
}