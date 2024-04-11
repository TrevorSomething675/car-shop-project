using MainTz.Application.Models;

namespace MainTz.Application.Repositories
{
    public interface IBrandRepository
    {
        public Task<List<Brand>> GetBrandsAsync();
        public Task<List<Brand>> GetBrandsByNameAsync(string brandName);

        public Task CreateAsync();
    }
}