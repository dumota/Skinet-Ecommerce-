using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Skinet_API.Entities;
using Skinet_Infrastructure.Data;

namespace Skinet_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly StoreContext _storeContext;

        public ProductController(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Getproducts() {
            var products = await _storeContext.Products.ToListAsync() ;
            return Ok(products);

        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<Product>> GetProductById(int Id)
        {
            var userById = await _storeContext.Products.FindAsync(Id);
            return Ok(userById);
        }
    }
}
