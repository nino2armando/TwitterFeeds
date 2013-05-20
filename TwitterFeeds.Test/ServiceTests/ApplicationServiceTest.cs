using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using FluentAssertions;
using TwitterFeeds.Models;
using TwitterFeeds.Repository;
using TwitterFeeds.Repository.Interface;
using TwitterFeeds.Service;
using TwitterFeeds.Service.Interface;
using Xunit;

namespace TwitterFeeds.Test.ServiceTests
{
    public class ApplicationServiceTest
    {
        private readonly IApplicationService _service;

        public ApplicationServiceTest()
        {
            IApiService apiService = new ApiService();
            IUrlBuilder urlBuilder = new UrlBuilder();
            ITwitterRepository repository = new TwitterRepository();
            this._service = new ApplicationService(repository, apiService, urlBuilder);
        }

        [Fact]
        public void Should_Receive_Valid_Account_Names()
        {
            string thirdAccount = ConfigurationManager.AppSettings["PayByPhone_UK"];

            string[] accounts = { null, thirdAccount };

            Exception ex = Assert.Throws<NoNullAllowedException>(() => this._service.GetAggregatedResults(accounts));
            Assert.Equal(ex.Message, "Not a valid account name");
        }

        [Fact]
        public void Should_get_tweets_for_specific_account()
        {
            var actual = this._service.GetTweets("pay_by_phone");

            var actualAccountName = actual.Select(a => a.User.ScreenName).First();

            Assert.Equal(actualAccountName, "pay_by_phone");
        }

        [Fact]
        public void Should_Return_Collection_of_Tweets()
        {
            var data = this._service.GetTweets(ConfigurationManager.AppSettings["pay_by_phone"]);
            Assert.Equal(data.GetType(), typeof(List<Tweet>));
        }

        [Fact]
        public void Should_Receive_Array_Of_String()
        {
            string firstAccount = ConfigurationManager.AppSettings["pay_by_phone"];
            string secondAccount = ConfigurationManager.AppSettings["PayByPhone"];
            string thirdAccount = ConfigurationManager.AppSettings["PayByPhone_UK"];

            string[] accounts = { firstAccount, secondAccount, thirdAccount };

            var data = this._service.GetAggregatedResults(accounts);

            data.Should().NotBe(null);
        }
    }
}
