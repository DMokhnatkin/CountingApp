using System.Threading.Tasks;
using CountingApp.Common;
using Microsoft.AspNetCore.Mvc;

namespace CountingApp.Server.Controllers
{
    [Produces("application/json")]
    [Route("api/user")]
    public class UserController : Controller
    {
        [HttpGet("current")]
        public Task<User> GetCurrentUser()
        {
            return null;
        }
    }
}