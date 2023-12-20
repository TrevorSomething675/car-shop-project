using MainTz.Database.Entities;
using System.Linq.Expressions;

namespace MainTz.Application.Repositories
{
    public interface ICarRepository
    {
        public Task<CarEntity> GetCarByIdAsync(int id);
        public Task<List<CarEntity>> GetCarsAsync(Expression<Func<CarEntity, bool>> filter = null);
        public Task CreateAsync(CarEntity car);
        public Task DeleteAsync(CarEntity car);
        public Task UpdateAsync(CarEntity car);
    }
}