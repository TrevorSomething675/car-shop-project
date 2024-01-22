namespace MainTz.Application.Services
{
    public interface IFavoriteCarService
    {
        public Task<bool> ChangeFavoriteCarAsync(int carId);
    }
}
