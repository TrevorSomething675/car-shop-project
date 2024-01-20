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
                var brandsEntity = await context.Brands.
                    Include(brand => brand.Models).
                    ToListAsync();
                var brands = _mapper.Map<List<Brand>>(brandsEntity);
                return brands;
            }
        }
    }
}