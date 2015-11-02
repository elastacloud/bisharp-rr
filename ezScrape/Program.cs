using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ezScrape
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {

                new WebClient().DownloadFile("http://www.radarvirtuel.com/file.json?tid=0.09024386689253151",
                  "C:\\tmp\\fd\\" + DateTime.Now.Ticks.ToString() + ".json");
                Thread.Sleep(15000);

            }
        }
    }
}
