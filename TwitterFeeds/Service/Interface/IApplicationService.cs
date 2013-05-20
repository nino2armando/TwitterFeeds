using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterFeeds.Models;

namespace TwitterFeeds.Service.Interface
{
    public interface IApplicationService
    {
        /// <summary>
        /// Gets the tweets.
        /// </summary>
        /// <param name="account">The account.</param>
        /// <returns></returns>
        IEnumerable<Tweet> GetTweets(string account);



        /// <summary>
        /// Gets the aggregated results.
        /// </summary>
        /// <param name="accounts">The accounts.</param>
        /// <returns></returns>
        TweetDataContract GetAggregatedResults(string[] accounts);
    }
}
