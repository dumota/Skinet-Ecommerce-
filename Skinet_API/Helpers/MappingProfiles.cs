using AutoMapper;
using Skinet_API.DTOs;
using Skinet_Core.Entities;

namespace Skinet_API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            //estudar sobre expresision em generics types
            CreateMap<Product, ProductReturnDTO>()
                .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
                .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name));
        }
    }
}
