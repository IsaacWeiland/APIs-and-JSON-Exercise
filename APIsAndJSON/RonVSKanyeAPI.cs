using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace APIsAndJSON
{
    internal static class RonVSKanyeAPI
    {
        static readonly HttpClient Client = new HttpClient();
        static readonly string KanyeUrl = "https://api.kanye.rest/";
        static readonly string RonUrl = "https://ron-swanson-quotes.herokuapp.com/v2/quotes";
        public static void Run()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"Ron: {RonQuote()}");
                Thread.Sleep(1500);
                Console.WriteLine($"Kanye: {KanyeQuote()}");
                Thread.Sleep(1500);
            }
        }

        private static string KanyeQuote()
        {
            var kanyeQuoteJson = Client.GetStringAsync(KanyeUrl).Result;
            var kanyeQuote = JsonObject.Parse(kanyeQuoteJson)["quote"].ToString();
            return kanyeQuote;
        }

        private static string RonQuote()
        {
            var ronQuoteJson = Client.GetStringAsync(RonUrl).Result; 
            var ronQuote = JsonArray.Parse(ronQuoteJson);
            return ronQuote[0].ToString();
        }
    }
}
