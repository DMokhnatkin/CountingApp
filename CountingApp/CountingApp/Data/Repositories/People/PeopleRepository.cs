using System;
using System.Threading.Tasks;
using CountingApp.Models;

namespace CountingApp.Data.Repositories.People
{
    public class PeopleRepository : IPeopleRepository
    {
        public async Task<Person[]> GetAvailablePeopleAsync()
        {
            await Task.Delay(500);
            return new []
            {
                new Person(Guid.NewGuid(), "Федор Михайлович"), 
                new Person(Guid.NewGuid(), "Василий Пупкин"), 
                new Person(Guid.NewGuid(), "Петр Иванович"), 
                new Person(Guid.NewGuid(), "Александр Иванов"),
                new Person(Guid.NewGuid(), "Иван Иванов")
            };
        }
    }
}
