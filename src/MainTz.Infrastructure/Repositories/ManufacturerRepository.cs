using AutoMapper;
using MainTa.Database.Context;
using MainTz.Application.Models;
using MainTz.Application.Repositories;
using MainTz.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace MainTz.Infrastructure.Repositories
{
    public class ManufacturerRepository : IManufacturerRepository
	{
		private readonly IDbContextFactory<MainContext> _dbContextFactory;
		private readonly IMapper _mapper;
		public ManufacturerRepository(IMapper mapper, IDbContextFactory<MainContext> dbContextFactory) 
		{
			_dbContextFactory = dbContextFactory;
			_mapper = mapper;
		}

		public async Task<Manufacturer> CreateManufacturer(Manufacturer manufacturer)
		{
			using (var context = _dbContextFactory.CreateDbContext())
			{
				var manufacturerToCreate = _mapper.Map<ManufacturerEntity>(manufacturer);
				var result = context.Manufacturers.Add(manufacturerToCreate);
				context.SaveChanges();
				return _mapper.Map<Manufacturer>(result.Entity);
			}
        }

        public async Task<Manufacturer> DeleteById(int id)
        {
			using (var context = _dbContextFactory.CreateDbContext())
			{
				var manufacturerEntity = context.Manufacturers.FirstOrDefault(m => m.Id == id);
				var result = context.Manufacturers.Remove(manufacturerEntity);
				context.SaveChanges();
				return _mapper.Map<Manufacturer>(result.Entity);
			}
        }

        public async Task<List<Manufacturer>> GetManufacturersAsync()
		{
			await using(var context = _dbContextFactory.CreateDbContext())
			{
				var manufacturersEntity = await context.Manufacturers
					.ToListAsync();
				var manufacturers = _mapper.Map<List<Manufacturer>>(manufacturersEntity);
				return manufacturers;
			}
		}
	}
}