using MainTz.Database.Entities;

namespace MainTz.Application.Repositories
{
    public interface ICarRepository
    {
        public Task<CarEntity> GetCarByIdAsync(int id);
        public Task<List<CarEntity>> GetCarsAsync();
        public Task CreateAsync(CarEntity car);
        public Task DeleteAsync(CarEntity car);
        public Task UpdateAsync(CarEntity car);
    }
}