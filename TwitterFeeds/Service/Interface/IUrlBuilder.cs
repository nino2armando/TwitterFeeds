using System.Collections.Specialized;

namespace TwitterFeeds.Service.Interface
{
    public interface IUrlBuilder
    {
        /// <summary>
        /// Gets the URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="queryString">The query string.</param>
        /// <returns></returns>
        string GetUrl(string url, NameValueCollection queryString);
    }
}
