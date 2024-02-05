using MainTz.Application.Models;

namespace MainTz.Application.Repositories
{
    public interface IImageRepository
    {
        public Task<Image> GetImageByIdAsync(int? id);
        public Task<List<Image>> GetImagesAsync();
    }
}
