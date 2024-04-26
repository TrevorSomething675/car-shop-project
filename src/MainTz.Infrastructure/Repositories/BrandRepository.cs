using MainTz.Application.Repositories;
using Microsoft.EntityFrameworkCore;
using MainTz.Application.Models;
using MainTa.Database.Context;
using AutoMapper;
using MainTz.Database.Entities;

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

        public async Task<Brand> CreateAsync(Brand brand)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var brandEntityToCreate = _mapper.Map<BrandEntity>(brand);
                var brandEntity = context.Brands.FirstOrDefault(b => b.Name == brandEntityToCreate.Name);

                if (brandEntity == null)
                {
                    var result = context.Brands.Add(brandEntityToCreate);
                    context.SaveChanges();
                    return _mapper.Map<Brand>(result.Entity);
                }
                return _mapper.Map<Brand>(brandEntity);
            }
        }

        public async Task<Brand> DeleteByIdAsync(int id)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var brandEntity = context.Brands.FirstOrDefault(b => b.Id == id);
                var result = context.Brands.Remove(brandEntity);
                context.SaveChanges();
                return _mapper.Map<Brand>(result.Entity);
            }
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