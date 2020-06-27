using Microsoft.Extensions.Logging;
using RestSharp;
using System.Threading.Tasks;

namespace ArtistLyrics.Core.Services
{
    public class LyricsService : RestApiService
    {
        private readonly ILogger<LyricsService> _logger;

        public LyricsService(IRestClient client, ILogger<LyricsService> logger) : base(client)
        {
            _logger = logger;
        }

        public async Task<string> GetLyricsAsync(string artistName, string songTitle)
        {
            var request = new RestRequest($"/v1/{artistName}/{songTitle}", Method.GET);
            var response = (await _client.ExecuteAsync<LyricResponse>(request)).Data;

            _logger.LogDebug("Lyrics {Lyrics}", response.Lyrics);

            return response.Lyrics;
        }
    }
}
