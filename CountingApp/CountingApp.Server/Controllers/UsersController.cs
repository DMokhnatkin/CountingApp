using System.Net.Http;
using CountingApp.Core.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CountingApp.Server.Controllers
{
    [Route("api/users")]
    [Authorize]
    public class UsersController : Controller
    {
        [HttpGet("names")]
        public IActionResult GetUserNames(string[] ids)
        {
            var httpClient = new HttpClient();
            return Ok();
        }

        [HttpGet("available")]
        public IActionResult GetAvailable()
        {
            return Ok(new []
            {
                new PersonDto {DisplayName = "test1", Id = "id1"},
                new PersonDto {DisplayName = "test2", Id = "id2"},
                new PersonDto {DisplayName = "test3", Id = "id3"},
                new PersonDto {DisplayName = "test4", Id = "id4"},
            });
        }
    }
}
