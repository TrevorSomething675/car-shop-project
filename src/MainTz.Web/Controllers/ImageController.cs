using MainTz.Web.ViewModels.ImageViewModels;
using MainTz.Application.Services;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace MainTz.Web.Controllers
{
    public class ImageController : Controller
    {
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;
        public ImageController(IImageService imageService, IMapper mapper)
        {
            _imageService = imageService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IResult> GetImageById([FromQuery]int id)
        {
            var image = await _imageService.GetBase64ImageByIdAsync(id);
            var imageResponse = _mapper.Map<ImageResponse>(image);
            return Results.Json(imageResponse);
        }
    }
}
