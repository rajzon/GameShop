using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GameShop.Domain.Dtos;
using GameShop.Domain.Dtos.AddressDtos;
using GameShop.Domain.Dtos.DeliveryOptDtos;
using GameShop.Domain.Dtos.UserDtos;
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
            CreateMap<Category,CategoryToReturnDto>();
            CreateMap<SubCategory,SubCategoryToReturnDto>();
            CreateMap<Language,LanguageToReturnDto>();
            CreateMap<Photo,PhotoForReturnDto>();      
            CreateMap<PhotoForCreationDto,Photo>();
            CreateMap<UserAccInfoToEditDto, User>()
                    .ForMember(dest => dest.UserName, x => x.MapFrom(src => src.Name));
            CreateMap<Product,Product>()
                    .ForMember(dest => dest.Photos, opt => opt.Ignore())
                    .ForMember(dest => dest.Stock, opt => opt.Ignore());
            CreateMap<UserAddressInfoForCreationDto, Address>();
            CreateMap<UserAddressInfoForEditDto, Address>();
            CreateMap<Address, UserAddressesForListDto>();
            CreateMap<DeliveryOpt, DeliveryOptToReturnDto>();

            /// Parsing Product To Updated Product with nested Objects in Product Class, using option Ignore for Ids which are tracked by Entity Framework
            // CreateMap<ProductSubCategory,ProductSubCategory>()
            //         .ForMember(dest => dest.ProductId, opt => opt.Ignore())
            //         .ForMember(dest => dest.SubCategoryId, opt => opt.Ignore()); 
            // CreateMap<ProductLanguage,ProductLanguage>()
            //         .ForMember(dest => dest.ProductId, opt => opt.Ignore())
            //         .ForMember(dest => dest.LanguageId, opt => opt.Ignore());      
            // CreateMap<Product,Product>()
            //         .ForMember(dest => dest.Id, opt => opt.Ignore())
            //         .ForMember(dest => dest.Photos, opt => opt.Ignore());   
            ///   
            
       
                
        }

        
    }
}