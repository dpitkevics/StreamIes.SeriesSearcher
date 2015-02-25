﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Xml;

namespace StreamIes.SeriesSearcher
{
    public class Searcher
    {
        public const String BASE_URL = "http://services.tvrage.com/feeds/{0}.php?{1}={2}";

        private Func<Results, int> callback;

        public Searcher(Func<Results, int> callback)
        {
            this.callback = callback;
        }

        public int SearchShowsByQuery(String query)
        {
            String requestUrl = String.Format(Searcher.BASE_URL, "search", "show", query);

            WebClient client = new WebClient();
            String xmlData = client.DownloadString(requestUrl);

            return this.callback(this.ParseShowsSearchResult(xmlData));
        }

        public Results ParseShowsSearchResult(String xmlData)
        {
            Results results = new Results();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlData);

            XmlNodeList showNodeList = xmlDoc.SelectNodes("/Results/show");
            foreach (XmlNode showNode in showNodeList)
            {
                Show show = new Show();
                show.showId = Convert.ToInt16(showNode.SelectSingleNode("showid").InnerText);
                show.name = showNode.SelectSingleNode("name").InnerText;
                show.link = showNode.SelectSingleNode("link").InnerText;
                show.country = showNode.SelectSingleNode("country").InnerText;
                show.started = Convert.ToInt16(showNode.SelectSingleNode("started").InnerText);
                show.ended = Convert.ToInt16(showNode.SelectSingleNode("ended").InnerText);
                show.seasons = Convert.ToInt16(showNode.SelectSingleNode("seasons").InnerText);
                show.status = showNode.SelectSingleNode("status").InnerText;
                show.classification = showNode.SelectSingleNode("classification").InnerText;

                XmlNodeList genreNodeList = showNode.SelectNodes("genres/genre");
                foreach (XmlNode genreNode in genreNodeList)
                {
                    Genre genre = new Genre();
                    genre.title = genreNode.InnerText;

                    show.genres.Add(genre);
                }

                results.showList.Add(show);
            }

            return results;
        }
    }
}
