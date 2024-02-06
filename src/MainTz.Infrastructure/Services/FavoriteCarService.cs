using MainTz.Application.Repositories;
using Microsoft.Extensions.Logging;
using MainTz.Application.Services;

namespace MainTz.Infrastructure.Services
{
    public class FavoriteCarService : IFavoriteCarService
    {
        private readonly ILogger<FavoriteCarService> _logger;
        private readonly IUserRepository _userRepository;
        private readonly ICarRepository _carRepository;
        public FavoriteCarService(ICarRepository carRepository, 
            IUserRepository userRepository, ILogger<FavoriteCarService> logger)
        {
            _logger = logger;
            _carRepository = carRepository;
            _userRepository = userRepository;
        }
        public async Task<int?> ChangeFavoriteCarAsync(int userId, int carId)
        {
            try
            {
                var user = await _userRepository.GetUserByIdAsync(userId);
                var carFromRepository = await _carRepository.GetCarByIdAsync(carId);
                var carToFavorite = user.Cars.FirstOrDefault(c => c.Id == carFromRepository.Id);
                if (carToFavorite != null)
                    user.Cars.Remove(carToFavorite);
                else
                    user.Cars.Add(carFromRepository);

                await _userRepository.UpdateAsync(user);

                return user.Id;
            }
            catch(Exception ex)
            {
                _logger.LogError("Ошибка при добавлении машниы в избранное {ex.Message}", ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}