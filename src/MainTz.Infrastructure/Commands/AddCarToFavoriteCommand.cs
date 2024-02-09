using MainTz.Application.Repositories;
using MediatR;

namespace MainTz.Web.Commands
{
    public class AddCarToFavoriteCommand : IRequest<int?>
    {
        public int UserId { get; set; }
        public int CarId { get; set; }

        public class AddCarToFavoriteCommandHandler : IRequestHandler<AddCarToFavoriteCommand, int?>
        {
            private readonly IUserRepository _userRepository;
            private readonly ICarRepository _carRepository;
            public AddCarToFavoriteCommandHandler(ICarRepository carRepository, IUserRepository userRepository) 
            {
                _userRepository = userRepository;
                _carRepository = carRepository;
            }

            public async Task<int?> Handle(AddCarToFavoriteCommand request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetUserByIdAsync(request.UserId);
                var carFromRepository = await _carRepository.GetCarByIdAsync(request.CarId);
                var carToFavorite = user.Cars.FirstOrDefault(c => c.Id == carFromRepository.Id);
                int? resultCarId;
                if (carToFavorite != null)
                {
                    user.Cars.Remove(carToFavorite);
                    resultCarId = carToFavorite.Id;
                }
                else
                {
                    user.Cars.Add(carFromRepository);
                    resultCarId = carFromRepository.Id;
                }

                await _userRepository.UpdateAsync(user);

                if (resultCarId != null)
                    return resultCarId;
                else
                    throw new Exception("Не удалось добавить машину в избранное");
            }
        }
    }
}