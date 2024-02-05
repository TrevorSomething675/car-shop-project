using MainTz.Application.Models;

namespace MainTz.Application.Services
{
    public interface IImageService
    {
        public Task<Image> GetImageByIdAsync(int? id);
        public Task<Image> GetBase64ImageByIdAsync(int? id);
        public Task<List<Image>> GetImagesAsync();
    }
}
