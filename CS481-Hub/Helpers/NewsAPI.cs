using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Script.Serialization;

namespace CS481_Hub.Helpers
{
    public class NewsAPI
    {

        private String sourceName;
        private String title;
        private String url;

        public NewsAPI(int index)
        {
            this.sourceName = returnSource(index);
            this.title = returnTitle(index);
            this.url = returnURL(index);
        }

        public JObject NewsJSON()
        {

            JObject newsJson = new JObject();
            string url = "https://newsapi.org/v2/top-headlines?country=us&apiKey=209abc6b164e421f94b644cd5a81a8ac";
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36");
                var response = httpClient.GetStringAsync(new Uri(url)).Result;
                newsJson = JObject.Parse(response);
            }
            return newsJson;
        }

        public int returnTotalResults()
        {
            int total = 0;

            JObject newsJson = NewsJSON();
            total = (int)newsJson["totalResults"];
            return total;
        }

        public String returnSource(int index)
        {
            JObject news = NewsJSON();
            return (string)news["articles"][index]["source"]["name"];

        }

        public String returnTitle(int index)
        {
            JObject news = NewsJSON();
            return (string)news["articles"][index]["title"];
        }

        public String returnURL(int index)
        {
            JObject news = NewsJSON();
            return (string)news["articles"][index]["url"];
        }

        public String getSource()
        {
            return sourceName;
        }
        public String getTitle()
        {
            return title;
        }
        public String getURL()
        {
            return url;
        }
    }
}