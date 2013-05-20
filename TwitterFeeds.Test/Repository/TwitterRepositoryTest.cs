using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Moq;
using TwitterFeeds.Models;
using TwitterFeeds.Repository;
using TwitterFeeds.Repository.Interface;
using Xunit;

namespace TwitterFeeds.Test.Repository
{
    public class TwitterRepositoryTest
    {
        [Fact]
        public void Should_return_a_collection_of_tweet()
        {
            var repository = new Mock<ITwitterRepository>();
            repository.Setup(s => s.DeserializePayload(It.IsAny<string>())).Returns(TestVectors.TestTweets);

            var tweetCollection = repository.Object.DeserializePayload("payload");

            Assert.Equal(tweetCollection.GetType(), typeof(List<Tweet>));
        }

        [Fact]
        public void Should_have_same_length()
        {
            ITwitterRepository repository = new TwitterRepository();
            var actual = repository.AggregateTimeLine(TestVectors.TestTweetCollection());
            Assert.Equal(actual.Count(), TestVectors.TestTweets().Count());
        }

        [Fact]
        public void Should_have_same_members()
        {
            ITwitterRepository repository = new TwitterRepository();
            var actual = repository.AggregateTimeLine(TestVectors.TestTweetCollection());

            var fromTest = TestVectors.TestTweets().Where(a => a.Text == "Bad Cat").Select(a => a.User).First();
            var fromRepo = actual.Where(a => a.Text == "Bad Cat").Select(a => a.User).First();

            Assert.Equal(fromTest.Name, fromRepo.Name);
        }

        [Fact]
        public void Should_return_tweet_data_contract()
        {
            ITwitterRepository repository = new TwitterRepository();
            var actual = repository.AggregateTimeLine(TestVectors.TestTweetCollection());
            var filteredData = repository.FilterByCriteria(actual);

            Assert.Equal(filteredData.GetType(), typeof(TweetDataContract));
        }

        [Fact]
        public void TweetDataContract_should_match()
        {
            ITwitterRepository repository = new TwitterRepository();
            var actual = repository.AggregateTimeLine(TestVectors.TestTweetCollection());
            var filteredData = repository.FilterByCriteria(actual);

            var fromRepo = filteredData.TimeLine.First();
            var fromTest = TestVectors.TestTweets().First(a => a.User.Id == fromRepo.User.Id);

            fromRepo.User.Name.Should().BeEquivalentTo(fromTest.User.Name);
        }
    }
}
