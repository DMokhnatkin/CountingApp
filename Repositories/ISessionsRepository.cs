using CountingApp.Dto;
using System.Collections.Generic;

namespace CountingApp.Repositories
{
    public interface ISessionsRepository
    {
        IEnumerable<SessionDto> GetSessions();
    }
}