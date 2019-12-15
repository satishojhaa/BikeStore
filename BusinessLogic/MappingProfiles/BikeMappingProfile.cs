using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace BusinessLogic.MappingProfiles
{
    public class BikeMappingProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Models.Bike, DAO.Models.Bike>()
                .ForMember(dest=> dest.Brand_Name,opt=>opt.MapFrom(src=>src.BrandMake))
                .ForMember(dest=>dest.Category_Name,opt=>opt.MapFrom(src=>src.CategoryOfBike))
                .ForMember(dest=> dest.List_Price,opt=> opt.MapFrom(src=>src.Price))
                .ForMember(dest => dest.Model_Year,opt=>opt.MapFrom(src=> src.YearOfMfg))
                .ForMember(dest => dest.Product_Id, opt => opt.MapFrom(src=>src.BikeId))
                .ForMember(dest => dest.Product_Name,opt => opt.MapFrom(src => src.BikeName))
                .ForMember(dest => dest.Quantity,opt => opt.MapFrom(src => src.QuantityInStock))
                .ForMember(dest=> dest.Brand_Id,opt=>opt.Ignore())
                .ForMember(dest=>dest.Category_Id,opt=>opt.Ignore());

            CreateMap<DAO.Models.Bike, Models.Bike>()
                .ForMember(dest => dest.BrandMake, opt => opt.MapFrom(src => src.Brand_Name))
                .ForMember(dest => dest.CategoryOfBike, opt => opt.MapFrom(src => src.Category_Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.List_Price))
                .ForMember(dest => dest.YearOfMfg, opt => opt.MapFrom(src => src.Model_Year))
                .ForMember(dest => dest.BikeId, opt => opt.MapFrom(src => src.Product_Id))
                .ForMember(dest => dest.BikeName, opt => opt.MapFrom(src => src.Product_Name))
                .ForMember(dest => dest.QuantityInStock, opt => opt.MapFrom(src => src.Quantity));
        }
    }
}
