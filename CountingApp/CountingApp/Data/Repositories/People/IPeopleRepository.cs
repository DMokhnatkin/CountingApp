using System.Threading.Tasks;
using CountingApp.Core.Dto;

namespace CountingApp.Data.Repositories.People
{
    interface IPeopleRepository
    {
        Task<PersonDto[]> GetAsync(string[] id);

        Task<PersonDto[]> GetAvailablePeopleAsync();
    }
}
