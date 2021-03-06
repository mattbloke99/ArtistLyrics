﻿using System.Collections.Generic;
using System.Linq;

namespace ArtistLyrics.Core
{
    public class Artist
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Song> Songs { get; set; }

        public decimal AverageWordCount()
        {
            if (Songs == null || !Songs.Any())
            {
                return 0;

            }
            var totalWords = (decimal)Songs.Sum(o => o.LyricCount());

            return totalWords / Songs.Count();

        }
    }
}
