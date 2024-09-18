using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace APIsAndJSON
{
    internal class OpenWeatherMapAPI
    {
        static readonly HttpClient Client = new HttpClient();
        static readonly string AppSettingsText = File.ReadAllText("appsettings.json");
        static readonly string ApiKey = JObject.Parse(AppSettingsText)["key"].ToString();
        static readonly string Lat = JObject.Parse(AppSettingsText)["lat"].ToString();
        static readonly string Lon = JObject.Parse(AppSettingsText)["lon"].ToString();
        static readonly string Url =
            $"https://api.openweathermap.org/data/2.5/weather?lat={Lat}&lon={Lon}&appid={ApiKey}&units=imperial";

        public static void Run()
        {
            Where();
            Temperature();
            TempMinMax();
            Humidity();
        }

        private static void Longitude()
        {
            var asyncString = Client.GetStringAsync(Url).Result;
            var weatherReport = JObject.Parse(asyncString)["coord"]["lon"];
            Console.WriteLine($"Longitude: {weatherReport}"); 
        }

        private static void Latitude()
        {
            var asyncString = Client.GetStringAsync(Url).Result;
            var weatherReport = JObject.Parse(asyncString)["coord"]["lat"];
            Console.WriteLine($"Latitude: {weatherReport}"); 
        }

        private static void Temperature()
        {
            var asyncString = Client.GetStringAsync(Url).Result;
            var temperatureReport = JObject.Parse(asyncString)["main"]["temp"];
            Console.WriteLine($"Temperature: {temperatureReport} F");
            var tempFeels = JObject.Parse(asyncString)["main"]["feels_like"];
            Console.WriteLine($"Feels like: {tempFeels} F");
            Console.WriteLine();
        }

        private static void TempMinMax()
        {
            var asyncString = Client.GetStringAsync(Url).Result;
            var tempMin = JObject.Parse(asyncString)["main"]["temp_min"];
            Console.WriteLine($"Minimum for today: {tempMin} F");
            var tempMax = JObject.Parse(asyncString)["main"]["temp_max"];
            Console.WriteLine($"Maximum for today: {tempMax} F");
            Console.WriteLine();
        }

        public static void Where()
        {
            var asyncString = Client.GetStringAsync(Url).Result;
            var where = JObject.Parse(asyncString)["name"];
            Console.WriteLine($"City: {where} at");
            Longitude();
            Latitude();
            Console.WriteLine();
        }

        private static void Humidity()
        {
            var asyncString = Client.GetStringAsync(Url).Result;
            var weatherReport = JObject.Parse(asyncString)["main"]["humidity"];
            Console.WriteLine($"Humidity: {weatherReport}");
            Console.WriteLine();
        }
    }
}
