using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CountingApp.Server.Controllers
{
    [Route("api/people")]
    [Authorize]
    public class PeopleController : Controller
    {
        [HttpGet("name")]
        public IActionResult GetDisplayNames(string[] ids)
        {
            throw new NotImplementedException();
        }
    }
}
