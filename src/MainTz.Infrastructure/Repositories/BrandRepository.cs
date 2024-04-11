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

        public async Task<List<Brand>> GetBrandsAsync()
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var brandEntities = await context.Brands
                    .ToListAsync();
                var brands = _mapper.Map<List<Brand>>(brandEntities);
                return brands;
            }
        }
        public async Task<List<Brand>> GetBrandsByNameAsync(string brandName)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var brandEntities = await context.Brands
                    .Where(brand => brand.Name == brandName)
                    .ToListAsync();
                var brands = _mapper.Map<List<Brand>>(brandEntities);
                return brands;
            }
        }
    }
}