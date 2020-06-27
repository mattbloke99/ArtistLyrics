using RestSharp;
using System;

namespace ArtistLyrics.Core.Services
{
    public class RestApiService
    {
        public readonly IRestClient _client;
        public RestApiService(IRestClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _client.UserAgent = "ApiCaller/1.0 (kaser47@hotmail.com)";
        }
    }
}
