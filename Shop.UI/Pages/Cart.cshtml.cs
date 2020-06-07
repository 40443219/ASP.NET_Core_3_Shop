using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

using Shop.Application.Cart;

namespace Shop.UI.Pages
{
    public class CartModel : PageModel
    {
        public IEnumerable<GetCart.Response> Cart { get; set; }  

        public IActionResult OnGet([FromServices] GetCart getCart)
        {
            Cart = getCart.Do();

            return Page();
        }  
    }
}