using System;
using System.Collections.Generic;
using TwitterFeeds.Models;

namespace TwitterFeeds.Test
{
    public static class TestVectors
    {
        private static readonly Random random = new Random();

        public static List<IEnumerable<Tweet>> TestTweetCollection()
        {
            var collection = new List<IEnumerable<Tweet>>();
            collection.Add(TestTweets());
            return collection;
        }

        public static IEnumerable<Tweet> TestTweets()
        {          
            var tweets = new List<Tweet>()
                             {
                                 new Tweet() { Id = random.Next(1, 200), Text = "hello world", User = GetRandaomUser(random.Next(0, 2)), Entities = GetRandomEntities(random.Next(0, 2)) },
                                 new Tweet() { Id = random.Next(1, 200), Text = "Bad Cat", User = GetRandaomUser(random.Next(0, 2)), Entities = GetRandomEntities(random.Next(0, 2)) },
                                 new Tweet() { Id = random.Next(1, 200), Text = "Awsome dog", User = GetRandaomUser(random.Next(0, 2)), Entities = GetRandomEntities(random.Next(0, 2)) },
                                 new Tweet() { Id = random.Next(1, 200), Text = "cat dog", User = GetRandaomUser(random.Next(0, 2)), Entities = GetRandomEntities(random.Next(0, 2)) }
                             };
            return tweets;
        }

        public static User GetRandaomUser(int ran)
        {
           var users = new List<User>()
                            {
                                new User() { Id = 1, Name = "PayByPhone", ScreenName = "PayByPhone" },
                                new User() { Id = 2, Name = "PayByPhone", ScreenName = "Pay_By_Phone" },
                                new User() { Id = 3, Name = "PayByPhone", ScreenName = "PayByPhone_UK" }
                            };
            return users[ran];
        }

        public static UserMention GetRandaomUserMention(int ran)
        {
            var userMentions = new List<UserMention>()
                            {
                                new UserMention() { Id = 1, Name = "PayByPhone", ScreenName = "PayByPhone" },
                                new UserMention() { Id = 2, Name = "PayByPhone", ScreenName = "Pay_By_Phone" },
                                new UserMention() { Id = 3, Name = "PayByPhone", ScreenName = "PayByPhone_UK" }
                            };
            return userMentions[ran];
        }

        public static Entities GetRandomEntities(int ran)
        {
            var entities = new List<UserMention> { GetRandaomUserMention(ran) };

            return new Entities()
                       {
                           UserMentions = entities
                       };
        }
    }
}
