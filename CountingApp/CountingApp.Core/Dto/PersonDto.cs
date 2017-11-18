namespace CountingApp.Core.Dto
{
    public class PersonDto
    {
        public string Id { get; set; }

        public string DisplayName { get; set; }

        public PersonDto(string id, string displayName)
        {
            Id = id;
            DisplayName = displayName;
        }

        public PersonDto()
        { }
    }
}
