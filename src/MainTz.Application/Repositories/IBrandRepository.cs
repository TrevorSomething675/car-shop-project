using MainTz.Application.Models;

namespace MainTz.Application.Repositories
{
    public interface IBrandRepository
    {
        public Task<List<Brand>> GetBrandsWithModelsAsync();
        public Task<List<Brand>> GetBrandsWithModelsByNameAsync(string brandName);

        public Task CreateAsync();
    }
}