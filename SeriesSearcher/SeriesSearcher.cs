using System;
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

        public Func<Results, int> callback { get; set; }
        public Func<Show, int> callbackShow { get; set; }

        public Searcher()
        {
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
                show.showId = Convert.ToInt32(showNode.SelectSingleNode("showid").InnerText);
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

                show.imageUrl = this.RetrieveImageUrl(show.link);

                results.showList.Add(show);
            }

            return results;
        }

        public String RetrieveImageUrl(String link)
        {
            WebClient client = new WebClient();
            String htmlData = client.DownloadString(link);

            if (htmlData.Contains("http://images.tvrage.com/shows/"))
            {
                int stringStart, stringEnd;

                stringStart = htmlData.IndexOf("http://images.tvrage.com/shows/", 0);
                htmlData = htmlData.Substring(stringStart);
                stringEnd = htmlData.IndexOf("'>", 0);
                htmlData = htmlData.Substring(0, stringEnd);

                return htmlData;
            }

            return "";
        }

        public int SearchShowDataById(Show show)
        {
            String requestUrl = String.Format(Searcher.BASE_URL, "episode_list", "sid", show.showId);

            WebClient client = new WebClient();
            String xmlData = client.DownloadString(requestUrl);

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlData);

            XmlNodeList seasonNodeList = xmlDoc.SelectNodes("/Show/Episodelist/Season");
            foreach (XmlNode seasonNode in seasonNodeList)
            {
                Season season = new Season();
                season.number = Convert.ToInt16(seasonNode.Attributes.GetNamedItem("no").Value);

                XmlNodeList episodeNodeList = seasonNode.SelectNodes("episode");
                foreach (XmlNode episodeNode in episodeNodeList)
                {
                    Episode episode = new Episode();
                    episode.episodeNumber = Convert.ToInt16(episodeNode.SelectSingleNode("epnum").InnerText);
                    episode.episodeNumberInSeason = episodeNode.SelectSingleNode("seasonnum").InnerText;
                    try
                    {
                        episode.airDate = Convert.ToDateTime(episodeNode.SelectSingleNode("airdate").InnerText);
                    }
                    catch (FormatException exception)
                    {
                        episode.airDate = new DateTime(1970, 1, 1);
                    }
                    episode.title = episodeNode.SelectSingleNode("title").InnerText;

                    season.episodes.Add(episode);
                }

                show.seasonsList.Add(season);
            }

            return this.callbackShow(show);
        }
    }
}
