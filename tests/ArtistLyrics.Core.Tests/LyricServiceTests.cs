using ArtistLyrics.Core.Services;
using RestSharp;
using Xunit;
using System.Threading.Tasks;

namespace ArtistLyrics.Core.Tests
{
    public class LyricServiceTests
    {
        [Fact]
        public async Task LyricServiceTest()
        {
            var artistName = "Queen";
            var songTitle = "Radio Gaga";

            var client = new RestClient("https://api.lyrics.ovh");

            var musicBrainzService = new LyricsService(client);

            string lyrics = await musicBrainzService.GetLyricsAsync(artistName, songTitle);

            Assert.True(!string.IsNullOrEmpty(lyrics));
            Assert.StartsWith("I'd sit alone and watch your light", lyrics);
        }
    }
}
