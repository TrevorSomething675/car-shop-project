using MainTz.Application.Models;
using MainTz.Database.Entities;
using AutoMapper;

namespace MainTz.Infrastructure.Mappings.Profiles
{
    public class DescriptionPorifle : Profile
    {
        public DescriptionPorifle() 
        {
            CreateMap<DescriptionEntity, Description>().ReverseMap();
        }
    }
}