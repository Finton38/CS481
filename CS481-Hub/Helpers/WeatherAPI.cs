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
    public class WeatherAPI
    {
        //returns json content of weather data
        public Object getWeatherForcast(String Zipcode)
        {
            string Zip = Zipcode;
            string appid = "e25f979e086ee31d8dc0066b110258c7";
            
            string url = "http://api.openweathermap.org/data/2.5/weather?zip=" + Zip + ",us&appid=" + appid + "&units=imperial";
            //synchronous client.
            var client = new WebClient();
            //string from API content
            var content = client.DownloadString(url);
            var serializer = new JavaScriptSerializer();
            var jsonContent = serializer.Deserialize<Object>(content);
            return jsonContent;
        }
    }  
}
