using ArtistLyrics.Core.Services;
using RestSharp;
using Xunit;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;

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

            var musicBrainzService = new LyricsService(client, new NullLogger<LyricsService>());

            string lyrics = await musicBrainzService.GetLyricsAsync(artistName, songTitle);

            Assert.True(!string.IsNullOrEmpty(lyrics));
            Assert.StartsWith("I'd sit alone and watch your light", lyrics);
        }
    }
}
