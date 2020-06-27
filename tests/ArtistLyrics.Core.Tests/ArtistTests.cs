using Xunit;

namespace ArtistLyrics.Core.Tests
{
    public class ArtistTests
    {
        [Fact]
        public void ArtistAverageLyricCountWithNoLyricsTest()
        {
            var song = new Song { Title = "Moonlight Sonata" };

            Song[] songs = { song };

            var artist = new Artist { Songs = songs };

            var averageLyricCount = artist.AverageWordCount();

            Assert.Equal(0m, averageLyricCount);
        }
    }
}
