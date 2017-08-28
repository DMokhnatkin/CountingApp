using CountingApp.Dto;
using System;
using System.Collections.Generic;

namespace CountingApp.Repositories
{
    internal class SessionsRepository : ISessionsRepository
    {
        public IEnumerable<SessionDto> GetSessions()
        {
            return new SessionDto[] 
            {
                new SessionDto() { CreatedDateTime = new DateTime(2017, 1, 2) },
                new SessionDto() { CreatedDateTime = new DateTime(2017, 2, 4) },
                new SessionDto() { CreatedDateTime = new DateTime(2017, 4, 6) },
                new SessionDto() { CreatedDateTime = new DateTime(2017, 4, 2) },
                new SessionDto() { CreatedDateTime = new DateTime(2017, 3, 1) },
            };
        }
    }
}