using System;
using Newtonsoft.Json;
using TwitterFeeds.Infrastructure;


namespace TwitterFeeds.Models
{
    public class Tweet
    {
        public long Id { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
        
        [JsonConverter(typeof(ApiDateConverter))]
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("entities")]
        public Entities Entities { get; set; }
    }



}