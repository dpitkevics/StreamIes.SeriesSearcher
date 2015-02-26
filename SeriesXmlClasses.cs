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
    }

    public class Show
    {
        public int showId { get; set; }
        public String name { get; set; }
        public String link { get; set; }
        public String country { get; set; }
        public int started { get; set; }
        public Boolean ended { get; set; }
        public int seasons { get; set; }
        public String status { get; set; }
        public String classification { get; set; }
        public List<Genre> genres { get; set; }

        public Show()
        {
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