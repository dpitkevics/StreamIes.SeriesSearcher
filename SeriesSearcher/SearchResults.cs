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

        public Show()
        {
            genres = new List<Genre>();
        }
    }

    public class Genre
    {
        public String title { get; set; }

        public Genre()
        {
        }
    }
}