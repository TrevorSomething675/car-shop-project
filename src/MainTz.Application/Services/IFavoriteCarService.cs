namespace MainTz.Application.Services
{
    public interface IFavoriteCarService
    {
        public Task<bool> AddCarToFavoriteByCarIdAsync(int carId);
    }
}
