using System.Configuration;
using FluentAssertions;
using Xunit;

namespace TwitterFeeds.Test
{
    public class ConfigurationTest
    {
        [Fact]
        public void Verify_AppConfig_Has_ConsumerKey()
        {
            string value = ConfigurationManager.AppSettings["consumerKey"];
            value.Should().NotBeNull(value);
        }

        [Fact]
        public void Verify_AppConfig_Has_ConsumerSecret()
        {
            string value = ConfigurationManager.AppSettings["consumerSecret"];
            value.Should().NotBeNull(value);
        }

        [Fact]
        public void Verify_AppConfig_Has_RequestUrl()
        {
            string value = ConfigurationManager.AppSettings["RequestUrl"];
            value.Should().NotBeNull(value);
        }
        
    }
}
