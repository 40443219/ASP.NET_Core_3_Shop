using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

using Shop.Application.ProductsAdmin;

namespace Shop.UI.Controllers
{
    [Authorize(Policy = "Manager")]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetProducts([FromServices] GetProducts getProducts) => 
            Ok(getProducts.Do());

        [HttpGet("{id}")]
        public IActionResult GetProduct([FromServices] GetProduct getProduct, int id) => 
            Ok(getProduct.Do(id));

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromServices] CreateProduct createProduct, [FromBody] CreateProduct.Request vm) => 
            Ok(await(createProduct.Do(vm)));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromServices] DeleteProduct deleteProduct, int id) => 
            Ok(await (deleteProduct.Do(id)));

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromServices] UpdateProduct updateProduct, [FromBody] UpdateProduct.Request vm) => 
            Ok(await(updateProduct.Do(vm)));
    }
}