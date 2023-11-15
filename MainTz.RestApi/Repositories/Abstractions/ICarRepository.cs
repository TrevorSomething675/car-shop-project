using MainTz.RestApi.Data.Models.Entities;
using System.Linq.Expressions;

namespace MainTz.RestApi.Repositories.Abstractions
{
    public interface ICarRepository
    {
        public Task<Car> GetCar(Expression<Func<Car, bool>> filter);
        public Task<List<Car>> GetCars(Expression<Func<Car, bool>> filter);

        public Task Create(Car car);
        public Task Update(Car car);
        public Task Delete(Car car);
    }
}
