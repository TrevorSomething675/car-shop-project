namespace MainTz.Application.Services
{
    public interface IFavoriteCarService
    {
        public Task<int?> ChangeFavoriteCarAsync(int userId, int carId);
    }
}
