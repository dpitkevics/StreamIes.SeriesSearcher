using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesSearcher
{
    public class SeariesSearcher
    {
        public const String BASE_URL = "http://services.tvrage.com/feeds/%s.php?%s=%s";

        public SeariesSearcher()
        {

        }

        public String[] searchShowsByQuery(String query)
        {
            String[] returned = new String[3];
            returned[0] = "test1";
            returned[1] = "test2";
            returned[2] = "test3";

            return returned;
        }
    }
}
