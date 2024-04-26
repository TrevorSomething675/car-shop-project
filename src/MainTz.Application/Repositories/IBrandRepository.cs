using MainTz.Application.Models;

namespace MainTz.Application.Repositories
{
    public interface IBrandRepository
    {
        public Task<List<Brand>> GetBrandsAsync();
        public Task<List<Brand>> GetBrandsByNameAsync(string brandName);

        public Task<Brand> CreateAsync(Brand brand);
        public Task<Brand> DeleteByIdAsync(int id);
    }
}