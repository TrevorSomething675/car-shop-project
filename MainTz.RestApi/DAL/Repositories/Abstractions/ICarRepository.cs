using MainTz.RestApi.dal.Data.Models.Entities;

namespace MainTz.RestApi.DAL.Repositories.Abstractions
{
    public interface ICarRepository
    {
        public Task<Car> GetCarById(int id);
        public Task<List<Car>> GetCars();
        public Task Create(Car car);
        public Task Delete(Car car);
    }
}
