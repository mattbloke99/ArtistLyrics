using Microsoft.Extensions.Logging;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtistLyrics.Core.Services
{
    public class MusicBrainzService : RestApiService
    {
        private readonly ILogger<MusicBrainzService> _logger;

        public MusicBrainzService(IRestClient client, ILogger<MusicBrainzService> logger) : base(client)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Artist> GetArtistByNameAsync(string artistName)
        {
            var request = new RestRequest($"/ws/2/artist?limit=1&query={artistName}&fmt=json", Method.GET);

            MusicBrainzArtistsResponse musicBrainArtistsResponse = (await _client.ExecuteAsync<MusicBrainzArtistsResponse>(request)).Data;

            //Assuming we're only interested in the first artist
            var artist = musicBrainArtistsResponse.Artists.FirstOrDefault();

            _logger.LogDebug("Found artist: {@artist}", artist);

            return artist;
        }

        public async Task<IEnumerable<Song>> GetSongsByIdAsync(string id)
        {
            //Assuming only 10 songs
            var request = new RestRequest($"/ws/2/work/?limit=10&artist={id}&fmt=json", Method.GET);

            MusicBrainzArtistWorks musicBrainArtistsResponse = (await _client.ExecuteAsync<MusicBrainzArtistWorks>(request)).Data;

            var songs = musicBrainArtistsResponse.Works;

            var songTitles = songs.Select(o => o.Title);

            _logger.LogDebug("Found songs: {songs}", string.Join(", ", songTitles));

            return songs;
        }
    }
}
