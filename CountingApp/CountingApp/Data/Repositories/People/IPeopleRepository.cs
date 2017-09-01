using System.Threading.Tasks;
using CountingApp.Models;

namespace CountingApp.Data.Repositories.People
{
    interface IPeopleRepository
    {
        Task<Person[]> GetAvailablePeopleAsync();
    }
}
