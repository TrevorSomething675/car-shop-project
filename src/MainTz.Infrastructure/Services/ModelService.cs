using MainTz.Application.Repositories;
using MainTz.Application.Services;
using MainTz.Application.Models;

namespace MainTz.Infrastructure.Services
{
    public class ModelService : IModelService
    {
        private readonly IModelRepository _modelRepository;
        public ModelService(IModelRepository modelRepository) 
        {
            _modelRepository = modelRepository;
        }
        public async Task<List<Model>> GetModels()
        {
            var models = await _modelRepository.GetModelsAsync();
            return models;
        }
    }
}