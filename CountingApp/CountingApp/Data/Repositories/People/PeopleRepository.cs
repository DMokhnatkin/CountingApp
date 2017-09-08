using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CountingApp.Models;

namespace CountingApp.Data.Repositories.People
{
    public class PeopleRepository : IPeopleRepository
    {
        private Dictionary<Guid, Person> _people = new Dictionary<Guid, Person>()
        {
            { new Guid("36AF843B-F279-4A22-9782-B21EAAE642EF"), new Person(new Guid("36AF843B-F279-4A22-9782-B21EAAE642EF"), "Федор Михайлович") },
            { new Guid("F9570A81-EC96-44D0-BCC3-7F4B454E1A36"), new Person(new Guid("F9570A81-EC96-44D0-BCC3-7F4B454E1A36"), "Василий Пупкин") },
            { new Guid("4F5D7462-1985-4F87-B27A-789D15A3F1D1"), new Person(new Guid("4F5D7462-1985-4F87-B27A-789D15A3F1D1"), "Петр Иванович") },
            { new Guid("852059BC-F3C8-401E-BD10-6D6F8A5822B1"), new Person(new Guid("852059BC-F3C8-401E-BD10-6D6F8A5822B1"),  "Александр Иванов") },
            { new Guid("61887FFC-FE3D-4187-9072-A1E7CC6D870E"), new Person(new Guid("61887FFC-FE3D-4187-9072-A1E7CC6D870E"), "Иван Иванов") },
        };

        public async Task<Person> GetAsync(Guid id)
        {
            await Task.Delay(50);
            return _people[id];
        }

        public async Task<Person[]> GetAvailablePeopleAsync()
        {
            await Task.Delay(50);
            return _people.Values.ToArray();
        }
    }
}
