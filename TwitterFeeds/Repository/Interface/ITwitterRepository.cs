using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TwitterFeeds.Models;

namespace TwitterFeeds.Repository.Interface
{

    /// <summary>
    /// Twitter Repository
    /// </summary>
    public interface ITwitterRepository
    {
        /// <summary>
        /// Gets the user status.
        /// </summary>
        /// <param name="payload">The payload.</param>
        /// <returns></returns>
        IEnumerable<Tweet> DeserializePayload(string payload);

        /// <summary>
        /// Aggregates the time line.
        /// </summary>
        /// <param name="results">The results.</param>
        /// <returns></returns>
        IQueryable<Tweet> AggregateTimeLine(List<IEnumerable<Tweet>> results);

        /// <summary>
        /// Filters the by criteria.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <returns></returns>
        TweetDataContract FilterByCriteria(IQueryable<Tweet> collection,
                                           System.Linq.Expressions.Expression<Func<Tweet, bool>> filter = null,
                                           Func<IQueryable<Tweet>, IOrderedQueryable<Tweet>> orderBy = null);
    }
}
