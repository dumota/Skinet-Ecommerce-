using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Skinet_Core.Entities;
using Skinet_Core.Interfaces;
using Skinet_Infrastructure.Data;

namespace Skinet_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Getproducts() {
            var products = await _productRepository.GetAlProductsAsync();
            return Ok(products);

        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<Product>> GetProductById(int Id)
        {
            var userById = await _productRepository.GetProductById(Id);
                
            return Ok(userById);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<ProductBrand>>> GetProductBrands()
        {
            return Ok(await _productRepository.GetAllProductsBrandsAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<ProductType>>> GetProductType()
        {
            return Ok(await _productRepository.GetAllProductTypesAsync());
        }


    }
}
