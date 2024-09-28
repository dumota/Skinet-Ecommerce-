using AutoMapper;
using Skinet_API.DTOs;
using Skinet_Core.Entities;

namespace Skinet_API.Helpers
{
    public class ProductUrlResolver : IValueResolver<Product, ProductReturnDTO, string>
    {
        private readonly IConfiguration _config;
        public ProductUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(Product source, 
            ProductReturnDTO destination,
            string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.PictureUrl))
            {
                return _config["ApiUrl"] + source.PictureUrl;
            }

            return null;
        }
    }
}
