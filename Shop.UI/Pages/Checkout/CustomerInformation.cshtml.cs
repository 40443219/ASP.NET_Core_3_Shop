using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

using Shop.Application.Cart;

namespace Shop.UI.Pages.Checkout
{
    public class CustomerInformationModel : PageModel
    {
        [BindProperty]
        public AddCustomerInformation.Request CustomerInformation { get; set; }
        public CustomerInformationModel()
        {
        }
        public IActionResult OnGet([FromServices] GetCustomerInformation getCustomerInformation)
        {
            var information = getCustomerInformation.Do();

            if(information == null)
            {
                return Page();
            }

            return RedirectToPage("/Checkout/Payment");
        }

        public IActionResult OnPost([FromServices] AddCustomerInformation addCustomerInformation)
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }


            addCustomerInformation.Do(CustomerInformation);

            return RedirectToPage("/Checkout/Payment");
        }
    }
}