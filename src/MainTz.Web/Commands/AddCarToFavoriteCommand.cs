using MainTz.Application.Services;
using MediatR;

namespace MainTz.Web.Commands
{
    public class AddCarToFavoriteCommand : IRequest<int?>
    {
        public int UserId { get; set; }
        public int CarId { get; set; }

        public class AddCarToFavoriteCommandHandler : IRequestHandler<AddCarToFavoriteCommand, int?>
        {
            private readonly IFavoriteCarService _favoriteCarService;
            public AddCarToFavoriteCommandHandler(IFavoriteCarService favoriteCarService) 
            {
                _favoriteCarService = favoriteCarService;
            }

            public async Task<int?> Handle(AddCarToFavoriteCommand request, CancellationToken cancellationToken)
            {
                var addedCar = await _favoriteCarService.ChangeFavoriteCarAsync(request.UserId , request.CarId);
                if (addedCar != null)
                    return addedCar;
                else
                    throw new Exception("Не удалось добавить машину в избранное");
            }
        }
    }
}
