using System.Configuration;

namespace TwitterFeeds.Models
{
    public class OAuthProperties
    {
        public string ConsumerKey 
        {
            get
            {
                return ConfigurationManager.AppSettings["consumerKey"];
            }
        }

        public string ConsumerSecret 
        {
            get
            {
                return ConfigurationManager.AppSettings["consumerSecret"];
            }
        }

        public string Token 
        {
            get
            {
                return ConfigurationManager.AppSettings["token"];
            }
        }

        public string TokenSecret
        {
            get
            {
                return ConfigurationManager.AppSettings["tokenSecret"];
            }
        }
    }
}