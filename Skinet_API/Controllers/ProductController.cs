﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Skinet_API.DTOs;
using Skinet_API.Errors;
using Skinet_API.Helpers;
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

        /*  public async Task<ActionResult<IEnumerable<ProductReturnDTO>>> Getproducts(string? sort,
              int? brandId, int? typeId) {
              var spec = new ProductsWithTypesAndBrandsSpecificatoin(sort, brandId, typeId);
              var products = await _productRepo.ListAsync(spec);
              return Ok(_mapper
                  .Map<IEnumerable<Product>, IEnumerable<ProductReturnDTO>>(products));

          }*/
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductReturnDTO>>> GetProducts(
            [FromQuery] ProductSpecParams productParams)
        {
            var spec = new ProductsWithTypesAndBrandsSpecificatoin(productParams);
            var countSpec = new  ProductsWithFiltersForCountSpecification(productParams);

            var totalItens = await _productRepo.CountAsync(countSpec);
            var products = await _productRepo.ListAsync(spec);

            var data = _mapper.Map<IEnumerable<ProductReturnDTO>>(products);

            return Ok(new Pagination<ProductReturnDTO>(productParams.PageIndex,productParams.PageSize,
                totalItens,data));
        }
        


        [HttpGet("{Id}")]
        //colocando aqui o tipo do endpoint e personalizando seus retornos, fazendo com que o mesmo
        //método possa retornar tipos de obejtos diferentes
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductReturnDTO>> GetProductById(int Id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecificatoin(Id);
            var product = await _productRepo.GetEntitiesWithSpec(spec);
            if (product == null)
            {
                return NotFound(new ApiResponse(404));
            }
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
