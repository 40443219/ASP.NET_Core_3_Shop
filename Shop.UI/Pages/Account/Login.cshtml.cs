using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Shop.UI.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public LoginModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [BindProperty]
        public LoginViewModel Input { get; set; }

        public async Task<IActionResult> OnPost()
        {
            var result = await _signInManager.PasswordSignInAsync(Input.Username, Input.Password, false, false);

            if(result.Succeeded)
            {
                return RedirectToPage("/Admin/Index");
            }

            return Page();
        }
    }

     public class LoginViewModel
     {
         public string Username { get; set; }
         public string Password { get; set; }
     }
}