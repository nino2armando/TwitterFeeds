using System.Net.Http;
using Moq;
using TwitterFeeds.Models;
using TwitterFeeds.OAuth;
using TwitterFeeds.Service;
using TwitterFeeds.Service.Interface;
using Xunit;

namespace TwitterFeeds.Test.ServiceTests
{
    public class ApiServiceTest
    {
        [Fact]
        public void Test_http_client_status_after_request()
        {
            var oauthProperties = new OAuthProperties();
            var client = new HttpClient(new OAuthMessageHandler(new HttpClientHandler(), oauthProperties));

            IApiService service = new ApiService(client);

            string url = "https://api.twitter.com/1/statuses/user_timeline.json?include_entities=true&screen_name=pay_by_phone";

            var result = service.GetTwitterTimelines(url);

            Assert.NotNull(result);
        }

        [Fact]
        public void Get_twitter_timelines_returns_string()
        {
            var service = new Mock<IApiService>();
            service.Setup(s => s.GetTwitterTimelines(It.IsAny<string>())).Returns("payload");

            var result = service.Object.GetTwitterTimelines("testUrl");

            Assert.Equal(result.GetType(), typeof(string));
        }

    }
}
