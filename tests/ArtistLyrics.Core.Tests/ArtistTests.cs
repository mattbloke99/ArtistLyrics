using Xunit;

namespace ArtistLyrics.Core.Tests
{
    public class ArtistTests
    {
        [Fact]
        public void ArtistAverageWordAverageTest()
        {
            var song = new Song { Title = "Radio Gaga", Lyrics = "I'd sit alone and watch your light \n\n\n\n My only friend through teenage nights" };
            var song2 = new Song { Title = "Radio Gaga", Lyrics = "I've paid my dues\nTime after time\r\nI've done my" };

            Song[] songs = { song, song2 };

            var artist = new Artist { Songs = songs };

            var averageLyricCount = artist.AverageWordCount();

            Assert.Equal(11.5m, averageLyricCount);
        }

        [Fact]
        public void ArtistWithNoSongsAverageTest()
        {
            var artist = new Artist();

            var averageLyricCount = artist.AverageWordCount();

            Assert.Equal(0m, averageLyricCount);
        }



        [Fact]
        public void ArtistAverageWordAverageWithNoLyricsTest()
        {
            var song = new Song { Title = "Moonlight Sonata" };

            Song[] songs = { song };

            var artist = new Artist { Songs = songs };

            var averageLyricCount = artist.AverageWordCount();

            Assert.Equal(0m, averageLyricCount);
        }
    }
}
