using System.Threading.Tasks;
using Autofac;
using CountingApp.Core.Config;
using CountingApp.Core.Dto;
using CountingApp.Services;
using Newtonsoft.Json;
using RestSharp;

namespace CountingApp.Data.Repositories.People
{
    public class HttpPeopleRepository : IPeopleRepository
    {
        private readonly RestClient _client;
        private static string _baseAddress = Uris.CoreServerUri + "/api/users";

        public HttpPeopleRepository()
        {
            _client = ApplicationIocContainer.CurrentContainer.Resolve<IAuthService>().GetRestClient();
        }

        public async Task<PersonDto[]> GetAsync(string[] ids)
        {
            //var request = new RestRequest($"{_baseAddress}/names", Method.GET);
            //foreach (var id in ids)
            //{
            //    request.AddQueryParameter("ids", id);
            //}
            //var res = await _client.ExecuteGetTaskAsync(request);

            ////var res = await _client.GetAsync("?" + string.Join("&ids=", ids).Substring(1));
            //return JsonConvert.DeserializeObject<PersonDto[]>(res.Content);
            return new PersonDto[0];
        }

        public async Task<PersonDto[]> GetAvailablePeopleAsync()
        {
            //var request = new RestRequest($"{_baseAddress}/available", Method.GET);
            //var res = await _client.ExecuteGetTaskAsync(request);

            //return JsonConvert.DeserializeObject<PersonDto[]>(res.Content);
            return new PersonDto[0];
        }
    }
}
