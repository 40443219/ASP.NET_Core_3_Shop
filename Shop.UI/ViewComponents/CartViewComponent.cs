using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;

using Shop.Application.Cart;
using Shop.Database;

namespace Shop.UI.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        private readonly GetCart _getCart;

        public CartViewComponent(GetCart getCart)
        {
            _getCart = getCart;
        }

        public async Task<IViewComponentResult> InvokeAsync(string view = "Default")
        {
            if(view.Contains("Nav"))
            {
                var totalValue = _getCart.Do().Sum(x => x.RealValue * x.Quantity);
                return View(view, $"${totalValue}");
            }

            return await Task.Run(() => View(view, _getCart.Do()));
        }
    }
}