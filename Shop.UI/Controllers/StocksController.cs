using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

using Shop.Application.StocksAdmin;

namespace Shop.UI.Controllers
{
    [Authorize(Policy = "Manager")]
    [Route("[controller]")]
    public class StocksController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetStocks([FromServices] GetStock getStock) => 
            Ok(getStock.Do());

        [HttpPost]
        public async Task<IActionResult> CreateStock([FromServices] CreateStock createStock, [FromBody] CreateStock.Request vm) => 
            Ok(await(createStock.Do(vm)));

        [HttpDelete("/{id}")]
        public async Task<IActionResult> DeleteStock([FromServices] DeleteStock deleteStock, int id) => 
            Ok(await (deleteStock.Do(id)));

        [HttpPut]
        public async Task<IActionResult> UpdateStock([FromServices] UpdateStocks updateStocks, [FromBody] UpdateStocks.Request vm) => 
            Ok(await(updateStocks.Do(vm)));
    }
}