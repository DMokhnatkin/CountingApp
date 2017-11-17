using CountingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CountingApp.Core.Dto;

namespace CountingApp.Data.Repositories.People
{
    public class PeopleRepository : IPeopleRepository
    {
        private Dictionary<Guid, PersonDto> _people = new Dictionary<Guid, PersonDto>
        {
            { new Guid("36AF843B-F279-4A22-9782-B21EAAE642EF"), new PersonDto{Id = "36AF843B-F279-4A22-9782-B21EAAE642EF", DisplayName = "Дима М" } },
            { new Guid("F9570A81-EC96-44D0-BCC3-7F4B454E1A36"), new PersonDto{Id = "F9570A81-EC96-44D0-BCC3-7F4B454E1A36", DisplayName ="Андрей Ш" } },
            { new Guid("4F5D7462-1985-4F87-B27A-789D15A3F1D1"), new PersonDto{Id = "4F5D7462-1985-4F87-B27A-789D15A3F1D1", DisplayName ="Антон М"} },
            { new Guid("852059BC-F3C8-401E-BD10-6D6F8A5822B1"), new PersonDto{Id = "852059BC-F3C8-401E-BD10-6D6F8A5822B1", DisplayName = "Рома В" } },
            { new Guid("61887FFC-FE3D-4187-9072-A1E7CC6D870E"), new PersonDto{Id = "61887FFC-FE3D-4187-9072-A1E7CC6D870E", DisplayName ="Катя В" } }
        };
        

        public async Task<PersonDto[]> GetAsync(string[] id)
        {
            await Task.Delay(50);
            return _people.Values.ToArray();
        }
        
        public async Task<PersonDto[]> GetAvailablePeopleAsync()
        {
            await Task.Delay(50);
            return _people.Values.ToArray();
        }
    }
}
