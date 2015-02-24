using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace StreamIes.SeriesSearcher
{
    public class Searcher
    {
        public const String BASE_URL = "http://services.tvrage.com/feeds/{0}.php?{1}={2}";

        public Searcher()
        {

        }

        public String[] SearchShowsByQuery(String query)
        {
            String requestUrl = String.Format(Searcher.BASE_URL, "search", "show", query);

            WebClient client = new WebClient();
            String xmlData = client.DownloadString(requestUrl);

            Console.WriteLine(xmlData);

            String[] returned = new String[3];
            returned[0] = "test1";
            returned[1] = "test2";
            returned[2] = "test3";

            return returned;
        }
    }
}
