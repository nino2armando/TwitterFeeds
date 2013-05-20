using System.Collections.Generic;
using Newtonsoft.Json;

namespace TwitterFeeds.Models
{
    public class Entities
    {
        public Entities()
        {
            this.UserMentions = new List<UserMention>();
        }

        [JsonProperty("user_mentions")]
        public IEnumerable<UserMention> UserMentions { get; set; }
    }
}