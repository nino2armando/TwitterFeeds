using System;

namespace TwitterFeeds.Service.Interface
{
    public interface IApiService : IDisposable
    {
        /// <summary>
        /// Gets the twitter timelines.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        string GetTwitterTimelines(string url);
    }
}
