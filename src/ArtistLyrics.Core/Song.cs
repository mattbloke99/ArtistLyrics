using System;
using System.Linq;

namespace ArtistLyrics.Core
{
    public class Song
    {
        public string Title { get; set; }
        public string Lyrics { get; set; }

        public int LyricCount()
        {
            string[] splitStrings = { " ", "\n", "\r" };

            var words = Lyrics?.Split(splitStrings, StringSplitOptions.RemoveEmptyEntries);

            return words == null ? 0 : words.Count();
        }
    }
}
