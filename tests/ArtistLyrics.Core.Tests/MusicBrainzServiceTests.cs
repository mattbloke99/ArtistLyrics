using ArtistLyrics.Core.Services;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using System.Threading.Tasks;

namespace ArtistLyrics.Core.Tests
{
    public class MusicBrainzServiceTests
    {
        [Fact]
        public async Task MusicBrainzServiceGetArtistTestAsync()
        {
            var artistName = "Queen";

            var client = new RestClient("https://musicbrainz.org");

            var musicBrainzService = new MusicBrainzService(client);

            Artist artist = await musicBrainzService.GetArtistByNameAsync(artistName);

            Assert.Equal(artistName, artist.Name);
            Assert.Equal("0383dadf-2a4e-4d10-a46a-e9e041da8eb3", artist.Id);
        }

        [Fact]
        public async Task MusicBrainzServiceGetSongsTest()
        {
            var id = "0383dadf-2a4e-4d10-a46a-e9e041da8eb3";

            var client = new RestClient("https://musicbrainz.org");

            var musicBrainzService = new MusicBrainzService(client);

            IEnumerable<Song> songs = await musicBrainzService.GetSongsByIdAsync(id);

            Assert.True(songs.Any());
            Assert.Contains(songs, o => !string.IsNullOrEmpty(o.Title));
        }
    }
}
