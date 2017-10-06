using Newtonsoft.Json;

namespace CountingApp.Models
{
    [JsonObject]
    class User
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string DisplayName { get; set; }
    }
}
