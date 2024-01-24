using MainTz.Application.Models;

namespace MainTz.Application.Services
{
    public interface IModelService
    {
        public Task<List<Model>> GetModels();
    }
}