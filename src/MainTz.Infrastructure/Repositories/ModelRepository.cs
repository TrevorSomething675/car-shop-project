using MainTz.Application.Repositories;
using Microsoft.EntityFrameworkCore;
using MainTz.Application.Models;
using MainTa.Database.Context;
using AutoMapper;

namespace MainTz.Infrastructure.Repositories
{
    public class ModelRepository : IModelRepository
    {
        private readonly IDbContextFactory<MainContext> _dbContextFactory;
        private readonly IMapper _mapper;
        public ModelRepository(IMapper mapper, IDbContextFactory<MainContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        public async Task<List<Model>> GetModelsAsync()
        {
            using(var context = _dbContextFactory.CreateDbContext())
            {
                var modelEntyties = context.Models.ToList();
                var models = _mapper.Map<List<Model>>(modelEntyties);

                return models;
            }
        }
    }
}