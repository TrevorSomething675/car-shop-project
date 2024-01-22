using MainTz.Application.Models;

namespace MainTz.Application.Repositories
{
    public interface ICarRepository
    {
        public Task<Car> GetCarByIdAsync(int id);
        public Task<Car> GetCarByNameAsync(string name);
        public Task<List<Car>> GetCarsAsync();
        public Task<Car> CreateAsync(Car car);
        public Task DeleteAsync(Car car);
        public Task UpdateAsync(Car car);
    }
}