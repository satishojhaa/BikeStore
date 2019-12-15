using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace BusinessLogic.MappingProfiles
{
    public class BrandMappingProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Models.Brand, DAO.Models.Brand>()
                .ForMember(dest => dest.Brand_Name, opt => opt.MapFrom(src => src.BrandName))
                .ForMember(dest => dest.Brand_Id, opt => opt.MapFrom(src => src.BrandId));

            CreateMap<DAO.Models.Brand, Models.Brand>()
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand_Name))
                .ForMember(dest => dest.BrandId, opt => opt.MapFrom(src => src.Brand_Id));
        }
    }
}
