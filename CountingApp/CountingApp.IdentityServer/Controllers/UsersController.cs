using System.Linq;
using CountingApp.IdentityServer.Data;
using CountingApp.IdentityServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CountingApp.IdentityServer.Controllers
{
    [Route("users")]
    [Authorize]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _applicationDbContext;

        public UsersController(UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _applicationDbContext = dbContext;
        }

        [HttpGet("names")]
        public IActionResult GetDisplayNames(string[] ids)
        {
            // We use _applicationDbContext instead of UserManager for optimization purpose (get all names in one query)
            // TODO: return names only for user in one group
            return Ok(_applicationDbContext.Users.Where(x => ids.Contains(x.Id)).Select(x => x.UserName).ToArray());
        }
    }
}
