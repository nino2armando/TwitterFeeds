using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Linq;
using TwitterFeeds.Models;
using TwitterFeeds.Repository.Interface;
using TwitterFeeds.Service.Interface;

namespace TwitterFeeds.Service
{
    public class ApplicationService : IApplicationService
    {
        private readonly ITwitterRepository _twitterRepository;
        private readonly IApiService _apiService;
        private readonly IUrlBuilder _urlBuilder;

        public ApplicationService(ITwitterRepository twitterRepository, IApiService apiService, IUrlBuilder urlBuilder)
        {
            this._twitterRepository = twitterRepository;
            this._apiService = apiService;
            this._urlBuilder = urlBuilder;
        }

        /// <summary>
        /// Gets the tweets.
        /// </summary>
        /// <param name="account">The account.</param>
        /// <returns></returns>
        public IEnumerable<Tweet> GetTweets(string account)
        {
            // prepare the url
            string url = ConfigurationManager.AppSettings["RequestUrl"];

            NameValueCollection parameteres = new NameValueCollection
                                                {
                                                    { "include_entities", "true" },
                                                    { "screen_name", account }
                                                };

            string encodedUrl = this._urlBuilder.GetUrl(url, parameteres);
            
            // make the call
            string payLoad = this._apiService.GetTwitterTimelines(encodedUrl);

            // map the payload
            var timeLines = this._twitterRepository.DeserializePayload(payLoad);

            return timeLines;
        }


        /// <summary>
        /// Gets the aggregated results.
        /// </summary>
        /// <param name="accounts">The accounts.</param>
        /// <returns></returns>
        public TweetDataContract GetAggregatedResults(string[] accounts)
        {
            for (int i = 0; i < accounts.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(accounts[i]))
                {
                    throw new NoNullAllowedException("Not a valid account name");
                }
            }

            List<IEnumerable<Tweet>> timeLineCollection = accounts.Select(this.GetTweets).ToList();

            var mergedTimelines = this._twitterRepository.AggregateTimeLine(timeLineCollection);

            var twoWeeksAgo = DateTime.Now.AddDays(-14);

            // pass our conditions two weeks period and order by date
            TweetDataContract results = 
                this._twitterRepository.FilterByCriteria(mergedTimelines,
                a => a.CreatedAt >= twoWeeksAgo, 
                a => a.OrderByDescending(d => d.CreatedAt));

            return results;
        }
    }
}