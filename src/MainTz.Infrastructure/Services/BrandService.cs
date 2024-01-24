using MainTz.Application.Repositories;
using MainTz.Application.Services;
using MainTz.Application.Models;

namespace MainTz.Infrastructure.Services
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        public BrandService(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }
        public async Task<List<Brand>> GetBrandsWithModelsByNameAsync(string brandName)
        {
            var bransWithModels = await _brandRepository.GetBrandsWithModelsByNameAsync(brandName);
            return bransWithModels;
        }
        public async Task<List<Brand>> GetBrandsWithModelsAsync()
        {
            var brandWithModels = await _brandRepository.GetBrandsWithModelsAsync();
            return brandWithModels;
        }
    }
}
