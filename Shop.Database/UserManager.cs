using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

using Shop.Domain.Infrastructure;

namespace Shop.Database
{
    public class UserManager : IUserManager
    {
        private readonly UserManager<IdentityUser> _userManager;
        public UserManager(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task CreateManagerUser(string username, string password)
        {
            var user = new IdentityUser()
            {
                UserName = username
            };

            await _userManager.CreateAsync(user, password);

            var userClaim = new Claim("Role", "Manager");

            await _userManager.AddClaimAsync(user, userClaim);
        }
    }
}