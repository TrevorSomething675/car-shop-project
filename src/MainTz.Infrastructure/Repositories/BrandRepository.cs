using MainTz.Application.Repositories;
using Microsoft.EntityFrameworkCore;
using MainTz.Application.Models;
using MainTa.Database.Context;
using AutoMapper;

namespace MainTz.Infrastructure.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly IDbContextFactory<MainContext> _dbContextFactory;
        private readonly IMapper _mapper;
        public BrandRepository(IMapper mapper, IDbContextFactory<MainContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        public async Task CreateAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Brand>> GetBrandsWithModelsAsync()
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var brandEntities = await context.Brands
                    .Include(brand => brand.Models)
                    .ToListAsync();
                var brands = _mapper.Map<List<Brand>>(brandEntities);
                return brands;
            }
        }
        public async Task<List<Brand>> GetBrandsWithModelsByNameAsync(string brandName)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var brandEntities = await context.Brands
                    .Include(brand => brand.Models)
                    .Where(brand => brand.Name == brandName)
                    .ToListAsync();
                var brands = _mapper.Map<List<Brand>>(brandEntities);
                return brands;
            }
        }
    }
}