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
                var contextUser = _contextAccessor?.HttpContext?.User.Identity;
                var user = await _userRepository.GetUserByNameAsync(contextUser.Name);
                var carFromRepository = await _carRepository.GetCarByIdAsync(carId);
                if (user.Cars.Select(car => car.Name).Contains(carFromRepository.Name))
                {
                    //var carToRemove = user.Cars.Where(car => car.Name == carFromRepository.Name).FirstOrDefault();
                    //user.Cars.Remove(carToRemove);
                    await _userRepository.RemoveCarFromUser(user, carFromRepository);
                }
                else
                {

                    user.Cars.Add(carFromRepository);
                    await _userRepository.UpdateAsync(user);
                }

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