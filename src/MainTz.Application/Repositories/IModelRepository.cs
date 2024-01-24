using MainTz.Application.Models;

namespace MainTz.Application.Repositories
{
    public interface IModelRepository
    {
        public Task<List<Model>> GetModelsAsync();
    }
}