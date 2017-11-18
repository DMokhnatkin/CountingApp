using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CountingApp.Core.Config;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CountingApp.Server.Controllers
{
    [Route("api/people")]
    [Authorize]
    public class PeopleController : Controller
    {
        [HttpGet("name")]
        public async Task<IActionResult> GetDisplayNames(string[] ids)
        {
            // Ask identity server for this information
            var httpClient = new HttpClient();
            var request = new StringBuilder( Uris.IdentityServerUri + "users/names?");
            foreach (var id in ids)
            {
                request.Append($"ids={id}&");
            }
            request.Remove(request.Length - 1, 1);
            var res = await httpClient.GetAsync(request.ToString());
            var data = await res.Content.ReadAsStringAsync();
            return Ok(data);
        }
    }
}
