using MainTz.Application.Repositories;
using MainTz.Application.Services;
using MainTz.Application.Models;

namespace MainTz.Infrastructure.Services
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;
        private readonly IMinioService _minioService;
        public ImageService(IImageRepository imageRepository, IMinioService minioService) 
        {
            _imageRepository = imageRepository;
            _minioService = minioService;
        }

        public async Task<Image> GetBase64ImageByIdAsync(int? id)
        {
            var image = await _imageRepository.GetImageByIdAsync(id);
            var imageBase64 = await _minioService.GetObjectAsync(image.Path);
            image.FileBase64String = imageBase64;
            return image;
        }

        public async Task<Image> GetImageByIdAsync(int? id)
        {
            var image = await _imageRepository.GetImageByIdAsync(id);
            return image;
        }

        public async Task<List<Image>> GetImagesAsync()
        {
            var images = await _imageRepository.GetImagesAsync();
            return images;
        }
    }
}