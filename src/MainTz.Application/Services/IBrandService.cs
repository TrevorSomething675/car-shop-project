using MainTz.Application.Models;

namespace MainTz.Application.Services
{
    public interface IBrandService
    {
        public Task<List<Brand>> GetBrandsByNameAsync(string brandName);
        public Task<List<Brand>> GetBrandsAsync();
    }
}
