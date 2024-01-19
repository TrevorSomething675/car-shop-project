using MainTz.Application.Models.CarModels;
using MainTz.Database.Entities;
using AutoMapper;

namespace MainTz.Infrastructure.Mappings.Profiles
{
    public class ModelProfile : Profile
    {
        public ModelProfile()
        {
            CreateMap<Model, ModelEntity>().ReverseMap();
        }
    }
}