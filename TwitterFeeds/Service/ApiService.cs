using System;
using System.Net.Http;

using Microsoft.Build.Tasks;

using TwitterFeeds.Models;
using TwitterFeeds.OAuth;
using TwitterFeeds.Service.Interface;

namespace TwitterFeeds.Service
{
    public class ApiService : IApiService
    {       
        private readonly HttpClient _client;
        private readonly OAuthProperties _oauthProperties;
        private bool _disposed = false;

        public ApiService()
        {
            this._oauthProperties = new OAuthProperties();
            this._client = new HttpClient(new OAuthMessageHandler(new HttpClientHandler(), this._oauthProperties));
        }

        public ApiService(HttpClient client)
        {
            this._client = client;
        }

        /// <summary>
        /// Gets the twitter timelines.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns></returns>
        public string GetTwitterTimelines(string uri)
        {
            string result;
            try
            {
                result = this._client.GetStringAsync(uri).Result;
            }
            catch (Exception e)
            {       
                throw;
            }

            return result;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    this._client.Dispose();
                }
            }

            this._disposed = true;
        }
    }
}