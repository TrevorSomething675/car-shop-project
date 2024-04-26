using MainTz.Application.Models;

namespace MainTz.Application.Services
{
    public interface IBrandService
    {
        public Task<List<Brand>> GetBrandsByNameAsync(string brandName);
        public Task<List<Brand>> GetBrandsAsync();
        public Task<Brand> CreateBrandAsync(Brand brand);
        public Task<Brand> DeleteBrandByIdAsync(int id);
    }
}
