using System.Collections.Generic;
using System.Linq;

namespace ArtistLyrics.Core
{
    public class Artist
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Song> Songs { get; set; }

        public decimal AverageLyricCount()
        {
            var lyricSum = (decimal)Songs.Sum(o => o.LyricCount());

            return lyricSum / Songs.Count();
        }
    }
}
