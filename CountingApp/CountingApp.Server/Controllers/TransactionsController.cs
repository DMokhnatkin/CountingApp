using System;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CountingApp.Core.Dto;
using CountingApp.Server.DbModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CountingApp.Server.DbModels.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CountingApp.Server.Controllers
{
    [Route("api/transactions")]
    [Authorize]
    public class TransactionsController : Controller
    {
        private readonly CountingAppDbContext _dbContext;
        private readonly ILogger<TransactionsController> _logger;

        public TransactionsController(CountingAppDbContext dbContext, ILogger<TransactionsController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        // GET api/transactions
        [HttpGet]
        public IActionResult Get()
        {
            var userId = GetCurUserId();
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var z = _dbContext.TransactionDbModels.Where(x => x.UserId == userId).ToArray();
            return new OkObjectResult(z);
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var userId = GetCurUserId();
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var z = _dbContext.TransactionDbModels.SingleOrDefault(x => x.TransactionId == new Guid(id));
            if (z?.UserId != userId)
                return new NotFoundObjectResult(null);

            return new OkObjectResult(z);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TransactionDto transactionDto)
        {
            var userId = GetCurUserId();
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            if (transactionDto.UserId != null && transactionDto.UserId == userId)
                return new BadRequestObjectResult($"{nameof(transactionDto.UserId)} must be null or equal to user id (from oauth2 token)");
            transactionDto.UserId = userId;

            try
            {
                _dbContext.TransactionDbModels.Add(transactionDto.Unmap());
                await _dbContext.SaveChangesAsync();
                return Ok();
            }
            catch (DbUpdateException ex) when ((ex.InnerException as SqlException)?.Number == 2627)
            {
                // Violation of primary key
                return StatusCode(409);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Can't write to database");
                return StatusCode(500);
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody] TransactionDto transactionDto)
        {
            // Do i need this method ?
            return Forbid();
        }

        [HttpDelete]
        public IActionResult Delete()
        {
            // Do i need this method ?
            return Forbid();
        }

        private string GetCurUserId()
        {
            return User.FindFirstValue("sub");
        }
    }
}
