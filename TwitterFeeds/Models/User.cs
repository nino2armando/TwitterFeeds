using Newtonsoft.Json;

namespace TwitterFeeds.Models
{
    public class User
    {
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("screen_name")]
        public string ScreenName { get; set; }
    }
}