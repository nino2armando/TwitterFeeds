using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

using TwitterFeeds.Models;

namespace TwitterFeeds.OAuth
{
    public class OAuthMessageHandler : DelegatingHandler
    {
        private readonly OAuthProperties _oauthProperties;

        private readonly OAuthBase _oAuthBase;

        public OAuthMessageHandler(HttpMessageHandler innerHandler, OAuthProperties oAuthProperties)
            : base(innerHandler)
        {
            this._oauthProperties = oAuthProperties;
            this._oAuthBase = new OAuthBase();
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Compute OAuth header 
            string normalizedUri;
            string normalizedParameters;
            string authHeader;

            string signature = this._oAuthBase.GenerateSignature(
                request.RequestUri,
                this._oauthProperties.ConsumerKey,
                this._oauthProperties.ConsumerSecret,
                this._oauthProperties.Token,
                this._oauthProperties.TokenSecret,
                request.Method.Method,
                this._oAuthBase.GenerateTimeStamp(),
                this._oAuthBase.GenerateNonce(),
                out normalizedUri,
                out normalizedParameters,
                out authHeader);

            request.Headers.Authorization = new AuthenticationHeaderValue("OAuth", authHeader);
            return base.SendAsync(request, cancellationToken);
        }
    }
}