using System;
using System.Collections.Generic;

namespace StreamIes.SeriesSearcher
{
    public class Results
    {
        public List<Show> showList { get; set; }

        public Results()
        {
            showList = new List<Show>();
        }

        public override String ToString()
        {
            return Convert.ToString(showList.Count);
        }
    }

    public class Show
    {
        public int showId { get; set; }
        public String name { get; set; }
        public String link { get; set; }
        public String country { get; set; }
        public int started { get; set; }
        public int ended { get; set; }
        public int seasons { get; set; }
        public String status { get; set; }
        public String classification { get; set; }
        public String imageUrl { get; set; }
        public List<Genre> genres { get; set; }
        public List<Season> seasonsList { get; set; }

        public Show()
        {
            genres = new List<Genre>();
            seasonsList = new List<Season>();
        }

        public int GetEpisodeCount()
        {
            int count = 0;
            foreach (Season season in this.seasonsList)
            {
                count += season.episodes.Count;
            }

            return count;
        }
    }

    public class Genre
    {
        public String title { get; set; }

        public Genre()
        {
        }
    }

    public class Season
    {
        public int number { get; set; }
        public List<Episode> episodes { get; set; }

        public Show show { get; set; }

        public Season()
        {
            episodes = new List<Episode>();
        }
    }

    public class Episode
    {
        public int episodeNumber { get; set; }
        public String episodeNumberInSeason { get; set; }
        public DateTime airDate { get; set; }
        public String title { get; set; }

        public Season season { get; set; }
        public Show show { get; set; }
    }
}