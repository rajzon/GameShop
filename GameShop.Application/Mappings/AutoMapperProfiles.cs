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
            // CreateMap<ProductToEditDto,Product>()
            //     .ForMember(dest => dest.Photos.GetEnumerator().Current.Url).AfterMap(src => src.Photos);
            CreateMap<ProductToEditDto,Product>()
                .ForMember(dest => dest.Photos, opt => opt.MapFrom(src => new Photo {Url = src.Photos[0]}));
        }

        
    }
}