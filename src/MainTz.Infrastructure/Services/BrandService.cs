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
        public async Task<List<Brand>> GetBrandsByNameAsync(string brandName)
        {
            var bransWithModels = await _brandRepository.GetBrandsByNameAsync(brandName);
            return bransWithModels;
        }
        public async Task<List<Brand>> GetBrandsAsync()
        {
            var brandWithModels = await _brandRepository.GetBrandsAsync();
            return brandWithModels;
        }
    }
}
