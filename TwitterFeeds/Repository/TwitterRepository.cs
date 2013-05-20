using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using TwitterFeeds.Models;
using TwitterFeeds.Repository.Interface;

namespace TwitterFeeds.Repository
{
    public class TwitterRepository : ITwitterRepository
    {
        /// <summary>
        /// Gets the user status.
        /// </summary>
        /// <param name="payload">The payload.</param>
        /// <returns></returns>
        public IEnumerable<Tweet> DeserializePayload(string payload)
        {
            JsonSerializerSettings dateFormatSettings = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.IsoDateFormat
                };

            IEnumerable<Tweet> serializedResult = JsonConvert.DeserializeObject<List<Tweet>>(payload);

            return serializedResult;
        }

        /// <summary>
        /// Aggregates the time line.
        /// </summary>
        /// <param name="results">The results.</param>
        /// <returns></returns>
        public IQueryable<Tweet> AggregateTimeLine(List<IEnumerable<Tweet>> results)
        {         
            var mergedCollections = results.SelectMany(a => a);

            return mergedCollections.AsQueryable();
        }

        /// <summary>
        /// Filters the by criteria.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <returns></returns>
        public TweetDataContract FilterByCriteria(
            IQueryable<Tweet> collection,
            System.Linq.Expressions.Expression<Func<Tweet, bool>> filter = null,
            Func<IQueryable<Tweet>, IOrderedQueryable<Tweet>> orderBy = null)
        {
            IQueryable<Tweet> resultSet = collection;

            TweetDataContract dataContract = new TweetDataContract();

            if (filter != null)
            {
                resultSet = resultSet.Where(filter);
            }

            if (orderBy != null)
            {
                resultSet = orderBy(resultSet);
            }

            dataContract.TimeLine = resultSet;

            // make sure Tweet Body is not empty
            dataContract.TotalTweets = resultSet.Where(i => i.Text != string.Empty).GroupBy(i => i.User.ScreenName)
                                .Select(group => new
                                    {
                                        name = group.Key,
                                        count = group.Count()
                                    }).ToDictionary(key => key.name, value => value.count);
    
            // make sure enitities -> userMentions are NOT empty
            dataContract.UserMentions = resultSet.Where(i => i.Entities.UserMentions != null).GroupBy(i => i.User.ScreenName)
                                                .Select(group => new
                                                {
                                                    name = group.Key,
                                                    count = group.Sum(a => a.Entities.UserMentions.Count(w => w.ScreenName != @group.Key))
                                                }).ToDictionary(key => key.name, value => value.count);

            return dataContract;
        }
    }
}