using System.Collections.Generic;

namespace TwitterFeeds.Models
{
    public class TweetDataContract
    {
        public TweetDataContract()
        {
            this.TimeLine = new List<Tweet>();
        }

        public IEnumerable<Tweet> TimeLine { get; set; }

        public Dictionary<string, int> TotalTweets { get; set; }

        public Dictionary<string, int> UserMentions { get; set; }
    }
}