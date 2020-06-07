using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;

using Shop.Application.Cart;

namespace Shop.UI.Controllers
{
    [Route("[controller]/[action]")]
    public class CartController : Controller
    {
        [HttpPost("{stockId}")]
        public async Task<IActionResult> AddOne(int stockId, [FromServices] AddToCart addToCart)
        {
            var request = new AddToCart.Request
            {
                StockId = stockId,
                Quantity = 1
            };

            var result = await addToCart.Do(request);

            if(result) 
            {
                return Ok("Item Added to cart");
            }
            
            return BadRequest("Failed to add to cart");
        }

        [HttpPost("{stockId}/{quantity?}")]
        public async Task<IActionResult> Remove([FromServices] RemoveFromCart removeFromCart, int stockId, int quantity = 1)
        {
            var request = new RemoveFromCart.Request
            {
                StockId = stockId,
                Quantity = quantity
            };

            var result = await removeFromCart.Do(request);

            if(result) 
            {
                return Ok("Item removed to cart");
            }
            
            return BadRequest("Failed to remove from cart");
        }

        [HttpGet]
        public IActionResult GetCartComponent([FromServices] GetCart getCart)
        {
            var totalValue = getCart.Do().Sum(x => x.RealValue * x.Quantity);
            return PartialView("Components/Cart/Nav", $"${totalValue}");
        }

        [HttpGet]
        public IActionResult GetCartMobileComponent([FromServices] GetCart getCart)
        {
            var totalValue = getCart.Do().Sum(x => x.RealValue * x.Quantity);
            return PartialView("Components/Cart/NavTouch", $"${totalValue}");
        }

        [HttpGet]
        public IActionResult GetCartMain([FromServices] GetCart getCart)
        {
            var cart = getCart.Do();
            return PartialView("_CartPartial", cart);
        }
    }
}