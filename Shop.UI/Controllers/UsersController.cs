using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

using Shop.Application.UserAdmin;

namespace Shop.UI.Controllers
{
    [Authorize(Policy = "Admin")]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly CreateUser _createUser;

        public UsersController(CreateUser createUser)
        {
            _createUser = createUser;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody]CreateUser.Request requset)
        {
            await _createUser.Do(requset);

            return Ok();
        }
    }
}