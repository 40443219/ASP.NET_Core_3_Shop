using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

using Shop.Application.Products;
using Shop.Application.Cart;

namespace Shop.UI.Pages
{
    public class ProductModel : PageModel
    {
        public GetProduct.ProductViewModel Product { get; set; }
        [BindProperty]
        public AddToCart.Request CartViewModel { get; set; }

        public async Task<IActionResult> OnGet(string name, [FromServices] GetProduct getProduct)
        {
            Product = await getProduct.Do(name);
            if(Product == null)
            {
                return RedirectToPage("Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPost([FromServices] AddToCart addToCart)
        {
            await addToCart.Do(CartViewModel);

            return RedirectToPage("Cart");
        }
    }
}