using MainTz.Application.Models;

namespace MainTz.Application.Services
{
    public interface IBrandService
    {
        public Task<List<Brand>> GetBrandsWithModelsByNameAsync(string brandName);
        public Task<List<Brand>> GetBrandsWithModelsAsync();
    }
}
