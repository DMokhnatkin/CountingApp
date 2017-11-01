using CountingApp.Core.Dto;
using CountingApp.Models;

namespace CountingApp.Data.Mappers
{
    internal static class PersonMapper
    {
        public static Person Unmap(this PersonDto dto)
        {
            return new Person(dto.Id, dto.DisplayName);
        }
    }
}
