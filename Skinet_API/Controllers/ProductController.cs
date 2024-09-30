using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Skinet_API.DTOs;
using Skinet_Core.Entities;
using Skinet_Core.Interfaces;
using Skinet_Core.Specifications;
using Skinet_Infrastructure.Data;

namespace Skinet_API.Controllers
{
   
    public class ProductController : BaseApiController
    {

        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;
        private readonly IMapper _mapper;

        public ProductController(IGenericRepository<Product> productRepo, 
            IGenericRepository<ProductBrand> productBrandRepo, 
            IGenericRepository<ProductType> productTypeRepo,
            IMapper mapper)
        {
            _productRepo = productRepo;
            _productBrandRepo = productBrandRepo;
            _productTypeRepo = productTypeRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductReturnDTO>>> Getproducts() {
            var spec = new ProductsWithTypesAndBrandsSpecificatoin();
            var products = await _productRepo.ListAsync(spec);
            return Ok(_mapper
                .Map<IEnumerable<Product>, IEnumerable<ProductReturnDTO>>(products));

        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<ProductReturnDTO>> GetProductById(int Id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecificatoin(Id);
            var product = await _productRepo.GetEntitiesWithSpec(spec);

            return _mapper.Map<Product, ProductReturnDTO>(product);

        }

        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<ProductBrand>>> GetProductBrands()
        {
            return Ok(await _productBrandRepo.ListAllAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<ProductType>>> GetProductType()
        {
            return Ok(await _productTypeRepo.ListAllAsync());
        }


    }
}
