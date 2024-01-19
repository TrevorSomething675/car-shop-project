﻿using MainTz.Web.ViewModels.CarViewModels;
using MainTz.Application.Models.CarModels;
using AutoMapper;

namespace MainTz.Web.Mappings.Profiles
{
    public class CarProfile : Profile
    {
        public CarProfile()
        {
            CreateMap<CarRequest, Car>();
            CreateMap<Car, CarResponse>();
        }
    }
}