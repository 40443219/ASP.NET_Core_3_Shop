using System.Threading.Tasks;

using Shop.Domain.Infrastructure;

namespace Shop.Application.UserAdmin
{
    public class CreateUser
    {
        private readonly IUserManager _userManager;
        public CreateUser(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> Do(Request request)
        {
            await _userManager.CreateManagerUser(request.UserName, request.Password);

            return true;
        }

        public class Request
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }
    }
}