using RestSharp;
using System.Threading.Tasks;

namespace ArtistLyrics.Core.Services
{
    public class LyricsService : RestApiService
    {
        public LyricsService(IRestClient client) : base(client)
        {
        }

        public async Task<string> GetLyricsAsync(string artistName, string songTitle)
        {
            var request = new RestRequest($"/v1/{artistName}/{songTitle}", Method.GET);
            var response = (await _client.ExecuteAsync<LyricResponse>(request)).Data;

            return response.Lyrics;
        }
    }
}
