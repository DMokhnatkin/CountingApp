using System.Linq;
using System.Security.Claims;
using CountingApp.Server.DbModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CountingApp.Server.Controllers
{
    [Route("api/transactions")]
    [Authorize]
    public class TransactionsController : Controller
    {
        private readonly CountingAppDbContext _dbContext;

        public TransactionsController(CountingAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET api/transactions
        [HttpGet]
        public IActionResult Get()
        {
            var sub = User.FindFirstValue("sub");
            if (string.IsNullOrEmpty(sub))
                return Unauthorized();

            var z = _dbContext.TransactionDbModels.Where(x => x.UserId == sub).ToArray();
            return new OkObjectResult(z);
        }
    }
}
