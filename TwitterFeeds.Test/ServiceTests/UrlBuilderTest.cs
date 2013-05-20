using System.Collections.Specialized;
using System.Web;
using FluentAssertions;
using TwitterFeeds.Service;
using TwitterFeeds.Service.Interface;
using Xunit;

namespace TwitterFeeds.Test.ServiceTests
{
    public class UrlBuilderTest
    {
        [Fact]
        public void Query_strings_in_url_should_match()
        {
            IUrlBuilder urlBuilder = new UrlBuilder();

            NameValueCollection parametere = new NameValueCollection
                                                {
                                                    { "include_entities", "true" },
                                                    { "screen_name", "PayByPhone" }
                                                };

            string url = urlBuilder.GetUrl(string.Empty, parametere);

            NameValueCollection queryStrings = HttpUtility.ParseQueryString(url);

            queryStrings["include_entities"].Should().BeEquivalentTo(parametere["include_entities"]);
            queryStrings["screen_name"].Should().BeEquivalentTo(parametere["screen_name"]);
        }
    }
}
