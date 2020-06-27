using Xunit;

namespace ArtistLyrics.Core.Tests
{
    public class SongTests
    {
        [Fact]
        public void SongLyricCountTest()
        {
            var song = new Song { Title = "Radio Gaga", Lyrics = "I'd sit alone and watch your light \n My only friend through teenage nights" };

            var lyricTotal = song.LyricCount();

            Assert.Equal(13, lyricTotal);
        }

        [Fact]
        public void ArtistAverageLyricCountTest()
        {
            var song = new Song { Title = "Radio Gaga", Lyrics = "I'd sit alone and watch your light \n\n\n\n My only friend through teenage nights" };
            var song2 = new Song { Title = "Radio Gaga", Lyrics = "I've paid my dues\nTime after time\r\nI've done my" };

            Song[] songs = { song, song2 };

            var artist = new Artist { Songs = songs };

            var averageLyricCount = artist.AverageWordCount();

            Assert.Equal(11.5m, averageLyricCount);
        }

        [Fact]
        public void SongLyricCountNoLyricsTest()
        {
            var song = new Song { Title = "Moonlight Sonata" };

            var lyricTotal = song.LyricCount();

            Assert.Equal(0, lyricTotal);
        }
    }
}
