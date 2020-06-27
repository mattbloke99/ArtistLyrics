using Xunit;

namespace ArtistLyrics.Core.Tests
{
    public class SongTests
    {
        [Fact]
        public void SongWordCountTest()
        {
            var song = new Song { Title = "Radio Gaga", Lyrics = "I'd sit alone and watch your light\r\nMy only friend through teenage nights" };

            var lyricTotal = song.LyricCount();

            Assert.Equal(13, lyricTotal);
        }

        [Fact]
        public void SongWordCountNoLyricsTest()
        {
            var song = new Song { Title = "Moonlight Sonata" };

            var lyricTotal = song.LyricCount();

            Assert.Equal(0, lyricTotal);
        }
    }
}
