using MainTz.Application.Repositories;
using Microsoft.Extensions.Logging;
using MainTz.Application.Services;
using Microsoft.AspNetCore.Http;

namespace MainTz.Infrastructure.Services
{
    public class FavoriteCarService : IFavoriteCarService
    {
        private readonly ILogger<FavoriteCarService> _logger;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUserRepository _userRepository;
        private readonly ICarRepository _carRepository;
        public FavoriteCarService(ICarRepository carRepository, 
            IUserRepository userRepository, IHttpContextAccessor contextAccessor,
            ILogger<FavoriteCarService> logger)
        {
            _logger = logger;
            _carRepository = carRepository;
            _userRepository = userRepository;
            _contextAccessor = contextAccessor;
        }
        public async Task<bool> ChangeFavoriteCarAsync(int carId)
        {
            try
            {
                var user = await _userRepository.GetUserByNameAsync(_contextAccessor?.HttpContext?.User.Identity.Name);
                var carFromRepository = await _carRepository.GetCarByIdAsync(carId);
                var carToFavorite = user.Cars.FirstOrDefault(c => c.Id == carFromRepository.Id);
                if (carToFavorite != null)
                    user.Cars.Remove(carToFavorite);
                else
                    user.Cars.Add(carFromRepository);

                await _userRepository.UpdateAsync(user);

                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError("Ошибка при добавлении машниы в избранное {ex.Message}", ex.Message);
                return false;
            }
        }
    }
}