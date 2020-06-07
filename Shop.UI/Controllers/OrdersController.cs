using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

using Shop.Application.OrdersAdmin;

namespace Shop.UI.Controllers
{
    [Authorize(Policy = "Manager")]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        // private readonly ApplicationDbContext _ctx;
        // public OrdersController(ApplicationDbContext ctx)
        // {
        //     _ctx = ctx;
        // }

        // [HttpGet]
        // public async Task<IActionResult> GetOrders(int status) => Ok(await (new GetOrders(_ctx).Do(status)));

        // [HttpGet("{id}")]
        // public async Task<IActionResult> GetOrder(int id) => Ok(await (new GetOrder(_ctx).Do(id)));

        // [HttpPut("{id}")]
        // public async Task<IActionResult> updateOrder(int id) => Ok(await (new UpdateOrder(_ctx).Do(id)));

        [HttpGet]
        public IActionResult GetOrders(int status, [FromServices] GetOrders getOrders) => 
            Ok(getOrders.Do(status));

        [HttpGet("{id}")]
        public IActionResult GetOrder(int id, [FromServices] GetOrder getOrder) => 
            Ok(getOrder.Do(id));

        [HttpPut("{id}")]
        public async Task<IActionResult> updateOrder(int id, [FromServices] UpdateOrder updateOrder)
        {
            var result = await updateOrder.Do(id) > 0;
            if(result)
            {
                return Ok();
            }

            return BadRequest();
        }   
    }
}