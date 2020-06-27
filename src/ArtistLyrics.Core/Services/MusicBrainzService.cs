using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtistLyrics.Core.Services
{
    public class MusicBrainzService : RestApiService
    {
        public MusicBrainzService(IRestClient client) : base(client)
        {
        }

        public async Task<Artist> GetArtistByNameAsync(string artistName)
        {
            var request = new RestRequest($"/ws/2/artist?limit=1&query={artistName}&fmt=json", Method.GET);
            MusicBrainArtistsResponse musicBrainArtistsResponse = (await _client.ExecuteAsync<MusicBrainArtistsResponse>(request)).Data;

            //Assuming we're only interested in the first artist
            return musicBrainArtistsResponse.Artists.FirstOrDefault();
        }

        public async Task<IEnumerable<Song>> GetSongsByIdAsync(string id)
        {
            //TODO do something witht the limit
            var request = new RestRequest($"/ws/2/work/?limit=10&artist={id}&fmt=json", Method.GET);
            MusicBrainArtistWorks musicBrainArtistsResponse = (await _client.ExecuteAsync<MusicBrainArtistWorks>(request)).Data;

            return musicBrainArtistsResponse.Works;
        }
    }
}
