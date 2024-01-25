using MainTz.Application.Models;

namespace MainTz.Application.Repositories
{
    public interface ICarRepository
    {
        public Task<Car> GetCarByIdAsync(int id);
        public Task<Car> GetCarByNameAsync(string name);
        public Task<List<Car>> GetCarsAsync(int userId, int? pageNumber);
        public Task<List<Car>> GetCarsWithHiddenAsync();
        public Task<Car> CreateAsync(Car car);
        public Task<Car> UpdateAsync(Car car);
        public Task RemoveCarByIdAsync(int id);
    }
}