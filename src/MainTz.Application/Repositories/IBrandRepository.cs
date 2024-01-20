using MainTz.Application.Models;

namespace MainTz.Application.Repositories
{
    public interface IBrandRepository
    {
        public Task<List<Brand>> GetBrandsWithModelsAsync();
        public Task CreateAsync();
    }
}