using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace BusinessLogic.MappingProfiles
{
    public class CategoryMappingProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<DAO.Models.Category, Models.Category>()
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Category_Id))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category_Name));
            CreateMap<Models.Category, DAO.Models.Category>()
                .ForMember(dest => dest.Category_Id, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.Category_Name, opt => opt.MapFrom(src => src.CategoryName));
        }
    }
}
