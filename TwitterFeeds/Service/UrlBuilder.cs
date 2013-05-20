using System;
using System.Web;
using TwitterFeeds.Service.Interface;

namespace TwitterFeeds.Service
{
    public class UrlBuilder : IUrlBuilder
    {
        /// <summary>
        /// Gets the URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="queryString">The query string.</param>
        /// <returns></returns>
        public string GetUrl(string url, System.Collections.Specialized.NameValueCollection queryString)
        {
            string apiUrl = url + "?" + 
                string.Join("&", 
                Array.ConvertAll(queryString.AllKeys,
                        key => string.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(queryString[key]))));

            return HttpUtility.UrlDecode(apiUrl);
        }
    }
}