using AutoMapper;
using GameShop.Domain.Dtos;
using GameShop.Domain.Model;

namespace GameShop.Application.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            
            CreateMap<RequirementsForCreationDto,Requirements>();
            CreateMap<RequirementsForEditDto,Requirements>();
            // CreateMap<ProductToEditDto,Product>()
            //     .ForMember(dest => dest.Photos, opt => opt.Ignore())
            //     .ForMember(dest => dest.Languages, opt => opt.Ignore())
            //     .ForMember(dest => dest.SubCategories, opt => opt.Ignore());
            // CreateMap<ProductToEditDto,Product>()
            //     .ForMember(dest => dest.Photos, opt => opt.MapFrom(src => new Photo {Url = src.Photos[0]}));
        }

        
    }
}