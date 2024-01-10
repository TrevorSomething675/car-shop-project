using MainTz.Application.Models.CarEntities;

namespace MainTz.Application.Repositories
{
    public interface ICarRepository
    {
        public Task<Car> GetCarByIdAsync(int id);
        public Task<List<Car>> GetCarsAsync();
        public Task CreateAsync(Car car);
        public Task DeleteAsync(Car car);
        public Task UpdateAsync(Car car);
    }
}